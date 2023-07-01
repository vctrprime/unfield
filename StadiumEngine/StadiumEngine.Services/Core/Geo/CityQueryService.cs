using StadiumEngine.Domain.Entities.Geo;
using StadiumEngine.Domain.Repositories.Geo;
using StadiumEngine.Domain.Services.Core.Geo;

namespace StadiumEngine.Services.Core.Geo;

internal class CityQueryService : ICityQueryService
{
    private readonly ICityRepository _repository;

    public CityQueryService( ICityRepository repository )
    {
        _repository = repository;
    }
    
    public async Task<List<City>> GetCitiesByFilterAsync( string searchString ) => await _repository.GetAsync( searchString, 10 );
}