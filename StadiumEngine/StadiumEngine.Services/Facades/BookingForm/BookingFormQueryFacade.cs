using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Services.Facades.BookingForm;

namespace StadiumEngine.Services.Facades.BookingForm;

internal class BookingFormQueryFacade : IBookingFormQueryFacade
{
    private readonly IFieldRepository _fieldRepository;
    private readonly IStadiumRepository _stadiumRepository;

    public BookingFormQueryFacade( IFieldRepository fieldRepository, IStadiumRepository stadiumRepository )
    {
        _fieldRepository = fieldRepository;
        _stadiumRepository = stadiumRepository;
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
}