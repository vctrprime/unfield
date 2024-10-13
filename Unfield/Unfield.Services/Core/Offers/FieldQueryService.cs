using Unfield.Domain.Entities.Offers;
using Unfield.Domain.Repositories.Offers;
using Unfield.Domain.Services.Core.Offers;

namespace Unfield.Services.Core.Offers;

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