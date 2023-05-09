using StadiumEngine.Domain.Entities.BookingForm;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Repositories.BookingForm;
using StadiumEngine.Domain.Repositories.Rates;
using StadiumEngine.Domain.Repositories.Settings;
using StadiumEngine.Domain.Services.Facades.BookingForm;
using StadiumEngine.Domain.Services.Models.BookingForm;
using StadiumEngine.Services.Facades.Services.BookingForm;

namespace StadiumEngine.Services.Facades.BookingForm;

internal class BookingFormQueryFacade : IBookingFormQueryFacade
{
    private readonly IBookingFormFieldRepositoryFacade _fieldRepositoryFacade;
    private readonly IStadiumMainSettingsRepository _stadiumMainSettingsRepository;
    private readonly IPriceRepository _priceRepository;
    private readonly IBookingRepository _bookingRepository;

    public BookingFormQueryFacade( IBookingFormFieldRepositoryFacade fieldRepositoryFacade,
        IStadiumMainSettingsRepository stadiumMainSettingsRepository, IPriceRepository priceRepository,
        IBookingRepository bookingRepository )
    {
        _fieldRepositoryFacade = fieldRepositoryFacade;
        _stadiumMainSettingsRepository = stadiumMainSettingsRepository;
        _priceRepository = priceRepository;
        _bookingRepository = bookingRepository;
    }

    public async Task<BookingFormData> GetBookingFormDataAsync( string? token, int? cityId, string? q, DateTime day, int currentHour )
    {
        List<Field> fields = await GetFieldsForBookingFormAsync( token, cityId, q );

        if ( !fields.Any() )
        {
            return new BookingFormData();
        }
        
        List<int> stadiumIds = fields.Select( x => x.StadiumId ).ToList();

        return new BookingFormData
        {
            IsForCity = String.IsNullOrEmpty( token ),
            Day = day,
            Fields = fields,
            Slots = await GetSlotsAsync( stadiumIds, currentHour, DateTime.Today.ToUniversalTime().Date == day.ToUniversalTime().Date ),
            Prices = await GetPricesAsync( stadiumIds ),
            Bookings = await GetBookingsAsync( day, stadiumIds )
        };
    }
    
    private async Task<List<Field>> GetFieldsForBookingFormAsync( string? token, int? cityId, string? q ) =>
        await _fieldRepositoryFacade.GetFieldsForBookingFormAsync( token, cityId, q );

    private async Task<Dictionary<int, List<(decimal, bool)>>> GetSlotsAsync( List<int> stadiumsIds, int currentHour, bool isToday )
    {
        List<StadiumMainSettings> settings = await _stadiumMainSettingsRepository.GetAsync( stadiumsIds );
        Dictionary<int, List<(decimal, bool)>> result = new Dictionary<int, List<(decimal, bool)>>();

        foreach ( StadiumMainSettings setting in settings )
        {
            List<(decimal, bool)> slots = new List<(decimal, bool)>();

            int start = isToday && currentHour > setting.OpenTime ? currentHour + 1 : setting.OpenTime;

            if ( start > setting.CloseTime )
            {
                return result;
            }

            for ( int i = setting.OpenTime; i <= setting.CloseTime; i++ )
            {
                slots.Add( (i, i >= start) );
                if ( i < setting.CloseTime  )
                {
                    slots.Add( (( decimal )( i + 0.5 ), i + 0.5 >= start) );
                }
            }

            result.Add( setting.StadiumId, slots );
        }

        return result;
    }

    private async Task<List<Price>> GetPricesAsync( List<int> stadiumsIds ) =>
        await _priceRepository.GetAllAsync( stadiumsIds );

    private async Task<List<Booking>> GetBookingsAsync( DateTime day, List<int> stadiumsIds ) =>
        await _bookingRepository.GetAsync( day, stadiumsIds );
    
}