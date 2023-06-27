#nullable enable
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Services.Application.Offers;

public interface IInventoryQueryService
{
    Task<List<Inventory>> GetByStadiumIdAsync( int stadiumId );
    Task<Inventory?> GetByInventoryIdAsync( int inventoryId, int stadiumId );
}