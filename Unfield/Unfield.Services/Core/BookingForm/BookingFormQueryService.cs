using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Entities.Offers;
using Unfield.Domain.Entities.Rates;
using Unfield.Domain.Entities.Settings;
using Unfield.Domain.Repositories.Accounts;
using Unfield.Domain.Repositories.Rates;
using Unfield.Domain.Repositories.Settings;
using Unfield.Domain.Services.Core.BookingForm;
using Unfield.Domain.Services.Models.BookingForm;
using Unfield.Services.Facades.BookingForm;
using Unfield.Services.Facades.Bookings;

namespace Unfield.Services.Core.BookingForm;

internal class BookingFormQueryService : IBookingFormQueryService
{
    private readonly IBookingFormFieldRepositoryFacade _fieldRepositoryFacade;
    private readonly IStadiumRepository _stadiumRepository;
    private readonly IMainSettingsRepository _mainSettingsRepository;
    private readonly IPriceRepository _priceRepository;
    private readonly IBookingFacade _bookingFacade;

    public BookingFormQueryService(
        IBookingFormFieldRepositoryFacade fieldRepositoryFacade,
        IStadiumRepository stadiumRepository,
        IMainSettingsRepository mainSettingsRepository,
        IPriceRepository priceRepository,
        IBookingFacade bookingFacade )
    {
        _fieldRepositoryFacade = fieldRepositoryFacade;
        _stadiumRepository = stadiumRepository;
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
        Stadium? stadium = !String.IsNullOrEmpty( token ) ? await _stadiumRepository.GetByTokenAsync( token ) : null;
        
        List<Field> fields = await GetFieldsForBookingFormAsync(
            stadium?.Id,
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
            StadiumId = stadium?.Id,
            IsForCity = String.IsNullOrEmpty( token ),
            Day = day,
            Fields = fields,
            Slots = await GetSlotsAsync(
                stadiumIds,
                currentHour,
                DateTime.Today.ToUniversalTime().Date == day.ToUniversalTime().Date ),
            Prices = await GetPricesAsync( stadiumIds ),
            Bookings = ( await _bookingFacade.GetAsync( day, stadiumIds ) )
                .Where( x => x.Number != currentBookingNumber )
                .ToList()
        };
    }

    private async Task<List<Field>> GetFieldsForBookingFormAsync(
        int? stadiumId,
        int? cityId,
        string? q,
        int? fieldId ) =>
        await _fieldRepositoryFacade.GetFieldsForBookingFormAsync(
            stadiumId,
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