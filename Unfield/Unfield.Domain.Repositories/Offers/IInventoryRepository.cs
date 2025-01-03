#nullable enable
using Unfield.Domain.Entities.Offers;

namespace Unfield.Domain.Repositories.Offers;

public interface IInventoryRepository
{
    Task<List<Inventory>> GetAllAsync( int stadiumId );
    Task<Inventory?> GetAsync( int inventoryId, int stadiumId );
    void Add( Inventory inventory );
    void Update( Inventory inventory );
    void Remove( Inventory inventory );
}