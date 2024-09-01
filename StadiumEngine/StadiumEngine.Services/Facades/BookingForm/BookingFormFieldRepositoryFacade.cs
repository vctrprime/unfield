using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Repositories.Offers;

namespace StadiumEngine.Services.Facades.BookingForm;

internal class BookingFormFieldRepositoryFacade : IBookingFormFieldRepositoryFacade
{
    private readonly IFieldRepository _fieldRepository;
    private readonly IStadiumRepository _stadiumRepository;

    public BookingFormFieldRepositoryFacade( IFieldRepository fieldRepository, IStadiumRepository stadiumRepository )
    {
        _fieldRepository = fieldRepository;
        _stadiumRepository = stadiumRepository;
    }


    public async Task<List<Field>> GetFieldsForBookingFormAsync(
        int? stadiumId,
        int? cityId,
        string? q,
        int? fieldId )
    {
        List<Field> fields = new List<Field>();

        if ( fieldId.HasValue )
        {
            Field? field = await _fieldRepository.GetAsync( fieldId.Value );
            if ( field != null )
            {
                fields.Add( field );
            }
            
            return fields;
        }

        if ( !stadiumId.HasValue )
        {
            if ( cityId.HasValue )
            {
                fields = await _fieldRepository.GetForCityAsync( cityId.Value, q );
            }
        }
        else
        {
            fields = await _fieldRepository.GetAllAsync( stadiumId.Value );
        }

        return fields;
    }
}