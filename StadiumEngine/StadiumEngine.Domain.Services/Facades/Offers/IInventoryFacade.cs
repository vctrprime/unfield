#nullable enable
using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.Common.Models;
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Services.Facades.Offers;

public interface IInventoryFacade
{
    Task<List<Inventory>> GetByStadiumId(int stadiumId);
    Task<Inventory?> GetByInventoryId(int inventoryId, int stadiumId);
    Task AddInventory(Inventory inventory, List<ImageFile> images, int legalId);
    Task UpdateInventory(Inventory inventory, List<ImageFile> images, List<SportKind> sportKinds);
    Task DeleteInventory(int inventoryId, int stadiumId);
}