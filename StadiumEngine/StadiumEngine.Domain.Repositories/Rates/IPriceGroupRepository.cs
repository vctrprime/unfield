#nullable enable
using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Domain.Repositories.Rates;

public interface IPriceGroupRepository
{
    Task<List<PriceGroup>> GetAllAsync( int stadiumId );
    Task<PriceGroup?> GetAsync( int priceGroupId, int stadiumId );
    void Add( PriceGroup priceGroup );
    void Update( PriceGroup priceGroup );
    void Remove( PriceGroup priceGroup );
}