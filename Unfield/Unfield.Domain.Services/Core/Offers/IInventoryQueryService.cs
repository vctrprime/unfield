#nullable enable
using Unfield.Domain.Entities.Offers;

namespace Unfield.Domain.Services.Core.Offers;

public interface IInventoryQueryService
{
    Task<List<Inventory>> GetByStadiumIdAsync( int stadiumId );
    Task<Inventory?> GetByInventoryIdAsync( int inventoryId, int stadiumId );
}