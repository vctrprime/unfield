using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services.Core.Accounts;

namespace StadiumEngine.Services.Core.Accounts;

internal class StadiumQueryService : IStadiumQueryService
{
    private readonly IStadiumRepository _repository;

    public StadiumQueryService( IStadiumRepository repository )
    {
        _repository = repository;
    }
    
    public async Task<List<Stadium>> GetAsync( int skip, int take ) =>
        await _repository.GetAsync( skip, take );
}