using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Entities.BookingForm;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Repositories.BookingForm;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Repositories.Rates;
using StadiumEngine.Domain.Repositories.Settings;
using StadiumEngine.Domain.Services.Facades.BookingForm;

namespace StadiumEngine.Services.Facades.BookingForm;

internal class BookingFormQueryFacade : IBookingFormQueryFacade
{
    private readonly IFieldRepository _fieldRepository;
    private readonly IStadiumRepository _stadiumRepository;
    private readonly IStadiumMainSettingsRepository _stadiumMainSettingsRepository;
    private readonly IPriceRepository _priceRepository;
    private readonly IBookingRepository _bookingRepository;

    public BookingFormQueryFacade( IFieldRepository fieldRepository, IStadiumRepository stadiumRepository,
        IStadiumMainSettingsRepository stadiumMainSettingsRepository, IPriceRepository priceRepository,
        IBookingRepository bookingRepository )
    {
        _fieldRepository = fieldRepository;
        _stadiumRepository = stadiumRepository;
        _stadiumMainSettingsRepository = stadiumMainSettingsRepository;
        _priceRepository = priceRepository;
        _bookingRepository = bookingRepository;
    }

    public async Task<List<Field>> GetFieldsForBookingFormAsync( string? token, int? cityId, string? q )
    {
        List<Field> fields = new List<Field>();
        if ( String.IsNullOrEmpty( token ) )
        {
            if ( cityId.HasValue )
            {
                fields = await _fieldRepository.GetForCityAsync( cityId.Value, q );
            }
        }
        else
        {
            Stadium? stadium = await _stadiumRepository.GetByTokenAsync( token );

            if ( stadium != null )
            {
                fields = await _fieldRepository.GetAllAsync( stadium.Id );
            }
        }

        return fields;
    }

    public async Task<Dictionary<int, List<decimal>>> GetSlotsAsync( List<int> stadiumsIds )
    {
        List<StadiumMainSettings> settings = await _stadiumMainSettingsRepository.GetAsync( stadiumsIds );
        Dictionary<int, List<decimal>> result = new Dictionary<int, List<decimal>>();

        foreach ( StadiumMainSettings setting in settings )
        {
            List<decimal> slots = new List<decimal>();
            for ( int i = setting.OpenTime; i <= setting.CloseTime; i++ )
            {
                slots.Add( i );
                if ( i < setting.CloseTime )
                {
                    slots.Add( ( decimal )( i + 0.5 ) );
                }
                
            }

            result.Add( setting.StadiumId, slots );
        }

        return result;
    }

    public async Task<List<Price>> GetPricesAsync( List<int> stadiumsIds ) =>
        await _priceRepository.GetAllAsync( stadiumsIds );

    public async Task<List<Booking>> GetBookingsAsync( DateTime day, List<int> stadiumsIds ) =>
        await _bookingRepository.GetAsync( day, stadiumsIds );
}