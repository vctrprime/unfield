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

    public async Task<List<Field>> GetByStadiumIdAsync( int stadiumId ) => await _fieldRepository.GetAllAsync( stadiumId );

    public async Task<Field?> GetByFieldIdAsync( int fieldId, int stadiumId ) =>
        await _fieldRepository.GetAsync( fieldId, stadiumId );
}