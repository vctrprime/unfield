#nullable enable
using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.Common.Models;
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Services.Facades.Offers;

public interface IInventoryCommandFacade
{
    Task AddInventory( Inventory inventory, List<ImageFile> images, int legalId );
    Task UpdateInventory( Inventory inventory, List<ImageFile> images, List<SportKind> sportKinds );
    Task DeleteInventory( int inventoryId, int stadiumId );
}