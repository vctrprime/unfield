using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Repositories.Accounts;
using Unfield.Domain.Services.Core.Accounts;

namespace Unfield.Services.Core.Accounts;

internal class StadiumQueryService : IStadiumQueryService
{
    private readonly IStadiumRepository _repository;

    public StadiumQueryService( IStadiumRepository repository )
    {
        _repository = repository;
    }
    
    public async Task<List<Stadium>> GetAsync( int skip, int take ) =>
        await _repository.GetAsync( skip, take );
    
    public async Task<Stadium?> GetAsync( string token ) => 
        await _repository.GetByTokenAsync( token );
}