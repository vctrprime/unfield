using Unfield.Domain.Entities.Geo;

namespace Unfield.Domain.Repositories.Geo;

public interface ICityRepository
{
    Task<List<City>> GetAsync( string q, int take );
}