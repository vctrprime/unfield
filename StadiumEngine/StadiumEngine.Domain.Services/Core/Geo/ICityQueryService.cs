using StadiumEngine.Domain.Entities.Geo;

namespace StadiumEngine.Domain.Services.Core.Geo;

public interface ICityQueryService
{
    Task<List<City>> GetCitiesByFilterAsync( string searchString );
}