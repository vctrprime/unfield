using StadiumEngine.Domain.Entities.Geo;

namespace StadiumEngine.Domain.Repositories.Geo;

public interface ICityRepository
{
    Task<List<City>> GetAsync( string q, int take );
}