using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services.Facades.Accounts;

namespace StadiumEngine.Services.Facades.Accounts;

internal class LegalQueryFacade : ILegalQueryFacade
{
    private readonly ILegalRepository _legalRepository;

    public LegalQueryFacade(
        ILegalRepository legalRepository )
    {
        _legalRepository = legalRepository;
    }

    public async Task<List<Legal>> GetLegalsByFilter( string searchString )
    {
        return await _legalRepository.GetByFilter( searchString );
    }
}