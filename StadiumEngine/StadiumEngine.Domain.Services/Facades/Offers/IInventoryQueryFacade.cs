#nullable enable
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Services.Facades.Offers;

public interface IInventoryQueryFacade
{
    Task<List<Inventory>> GetByStadiumIdAsync( int stadiumId );
    Task<Inventory?> GetByInventoryIdAsync( int inventoryId, int stadiumId );
}