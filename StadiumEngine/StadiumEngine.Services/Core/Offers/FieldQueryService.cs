using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Services.Core.Offers;

namespace StadiumEngine.Services.Core.Offers;

internal class FieldQueryService : IFieldQueryService
{
    private readonly IFieldRepository _fieldRepository;

    public FieldQueryService(
        IFieldRepository fieldRepository )
    {
        _fieldRepository = fieldRepository;
    }

    public async Task<List<Field>> GetByStadiumIdAsync( int stadiumId ) => await _fieldRepository.GetAllAsync( stadiumId );

    public async Task<Field?> GetByFieldIdAsync( int fieldId, int stadiumId ) =>
        await _fieldRepository.GetAsync( fieldId, stadiumId );
}