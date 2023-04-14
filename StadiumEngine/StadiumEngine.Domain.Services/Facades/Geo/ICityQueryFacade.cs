using StadiumEngine.Domain.Entities.Geo;

namespace StadiumEngine.Domain.Services.Facades.Geo;

public interface ICityQueryFacade
{
    Task<List<City>> GetCitiesByFilterAsync( string searchString );
}