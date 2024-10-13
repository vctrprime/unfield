using Unfield.Domain.Entities.Geo;

namespace Unfield.Domain.Services.Core.Geo;

public interface ICityQueryService
{
    Task<List<City>> GetCitiesByFilterAsync( string searchString );
}