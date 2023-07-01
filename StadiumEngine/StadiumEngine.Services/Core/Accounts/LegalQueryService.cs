using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services.Core.Accounts;

namespace StadiumEngine.Services.Core.Accounts;

internal class LegalQueryService : ILegalQueryService
{
    private readonly ILegalRepository _legalRepository;

    public LegalQueryService(
        ILegalRepository legalRepository )
    {
        _legalRepository = legalRepository;
    }

    public async Task<List<Legal>> GetLegalsByFilterAsync( string searchString ) =>
        await _legalRepository.GetByFilterAsync( searchString );
}