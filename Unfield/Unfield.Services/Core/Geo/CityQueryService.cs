using Unfield.Domain.Entities.Geo;
using Unfield.Domain.Repositories.Geo;
using Unfield.Domain.Services.Core.Geo;

namespace Unfield.Services.Core.Geo;

internal class CityQueryService : ICityQueryService
{
    private readonly ICityRepository _repository;

    public CityQueryService( ICityRepository repository )
    {
        _repository = repository;
    }
    
    public async Task<List<City>> GetCitiesByFilterAsync( string searchString ) => await _repository.GetAsync( searchString, 10 );
}