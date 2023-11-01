using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Repositories.Rates;
using StadiumEngine.Domain.Repositories.Settings;
using StadiumEngine.Domain.Services.Core.BookingForm;
using StadiumEngine.Domain.Services.Models.BookingForm;
using StadiumEngine.Services.Facades.BookingForm;
using StadiumEngine.Services.Facades.Bookings;

namespace StadiumEngine.Services.Core.BookingForm;

internal class BookingFormQueryService : IBookingFormQueryService
{
    private readonly IBookingFormFieldRepositoryFacade _fieldRepositoryFacade;
    private readonly IMainSettingsRepository _mainSettingsRepository;
    private readonly IPriceRepository _priceRepository;
    private readonly IBookingFacade _bookingFacade;

    public BookingFormQueryService(
        IBookingFormFieldRepositoryFacade fieldRepositoryFacade,
        IMainSettingsRepository mainSettingsRepository,
        IPriceRepository priceRepository,
        IBookingFacade bookingFacade )
    {
        _fieldRepositoryFacade = fieldRepositoryFacade;
        _mainSettingsRepository = mainSettingsRepository;
        _priceRepository = priceRepository;
        _bookingFacade = bookingFacade;
    }

    public async Task<BookingFormData> GetBookingFormDataAsync(
        string? token,
        int? cityId,
        int? fieldId,
        string? q,
        DateTime day,
        int currentHour,
        string currentBookingNumber )
    {
        List<Field> fields = await GetFieldsForBookingFormAsync(
            token,
            cityId,
            q,
            fieldId );

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
            Slots = await GetSlotsAsync(
                stadiumIds,
                currentHour,
                DateTime.Today.ToUniversalTime().Date == day.ToUniversalTime().Date ),
            Prices = await GetPricesAsync( stadiumIds ),
            Bookings = ( await _bookingFacade.GetAsync( day, stadiumIds ) ).Where( x => x.Number != currentBookingNumber )
                .ToList()
        };
    }

    private async Task<List<Field>> GetFieldsForBookingFormAsync(
        string? token,
        int? cityId,
        string? q,
        int? fieldId ) =>
        await _fieldRepositoryFacade.GetFieldsForBookingFormAsync(
            token,
            cityId,
            q,
            fieldId );

    private async Task<Dictionary<int, List<(decimal, bool)>>> GetSlotsAsync(
        List<int> stadiumsIds,
        int currentHour,
        bool isToday )
    {
        List<MainSettings> settings = await _mainSettingsRepository.GetAsync( stadiumsIds );
        Dictionary<int, List<(decimal, bool)>> result = new Dictionary<int, List<(decimal, bool)>>();

        foreach ( MainSettings setting in settings )
        {
            List<(decimal, bool)> slots = new List<(decimal, bool)>();

            int start = isToday && currentHour > setting.OpenTime ? currentHour + 1 : setting.OpenTime;

            if ( start > setting.CloseTime )
            {
                return result;
            }

            for ( int i = setting.OpenTime; i <= setting.CloseTime; i++ )
            {
                slots.Add( ( i, i >= start ) );
                if ( i < setting.CloseTime )
                {
                    slots.Add( ( ( decimal )( i + 0.5 ), i + 0.5 >= start ) );
                }
            }

            result.Add( setting.StadiumId, slots );
        }

        return result;
    }

    private async Task<List<Price>> GetPricesAsync( List<int> stadiumsIds ) =>
        await _priceRepository.GetAllAsync( stadiumsIds );
}