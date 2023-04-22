using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Repositories.Accounts;
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

    public BookingFormQueryFacade( IFieldRepository fieldRepository, IStadiumRepository stadiumRepository,
        IStadiumMainSettingsRepository stadiumMainSettingsRepository, IPriceRepository priceRepository )
    {
        _fieldRepository = fieldRepository;
        _stadiumRepository = stadiumRepository;
        _stadiumMainSettingsRepository = stadiumMainSettingsRepository;
        _priceRepository = priceRepository;
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

    public async Task<Dictionary<int, List<int>>> GetSlotsAsync( List<int> stadiumsIds )
    {
        List<StadiumMainSettings> settings = await _stadiumMainSettingsRepository.GetAsync( stadiumsIds );
        Dictionary<int, List<int>> result = new Dictionary<int, List<int>>();

        foreach ( StadiumMainSettings setting in settings )
        {
            List<int> slots = new List<int>();
            for ( int i = setting.OpenTime; i < setting.CloseTime; i++ )
            {
                slots.Add( i );
            }

            result.Add( setting.StadiumId, slots );
        }

        return result;
    }

    public Task<List<Price>> GetPrices( List<int> stadiumsIds ) => _priceRepository.GetAllAsync( stadiumsIds );
}