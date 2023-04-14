using StadiumEngine.Domain.Entities.Geo;
using StadiumEngine.Domain.Repositories.Geo;
using StadiumEngine.Domain.Services.Facades.Geo;

namespace StadiumEngine.Services.Facades.Geo;

internal class CityQueryFacade : ICityQueryFacade
{
    private readonly ICityRepository _repository;

    public CityQueryFacade( ICityRepository repository )
    {
        _repository = repository;
    }
    
    public async Task<List<City>> GetCitiesByFilterAsync( string searchString ) => await _repository.GetAsync( searchString, 10 );
}