using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Domain.Repositories.Rates;

public interface IPriceRepository
{
    Task<List<Price>> GetAllAsync( int stadiumId );
    void Add( IEnumerable<Price> prices );
    void Remove( IEnumerable<Price> prices );
}