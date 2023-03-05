using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Services.Facades.Offers;

namespace StadiumEngine.Services.Facades.Offers;

internal class FieldQueryFacade : IFieldQueryFacade
{
    private readonly IFieldRepository _fieldRepository;

    public FieldQueryFacade(
        IFieldRepository fieldRepository )
    {
        _fieldRepository = fieldRepository;
    }

    public async Task<List<Field>> GetByStadiumId( int stadiumId )
    {
        return await _fieldRepository.GetAll( stadiumId );
    }

    public async Task<Field?> GetByFieldId( int fieldId, int stadiumId )
    {
        return await _fieldRepository.Get( fieldId, stadiumId );
    }
}