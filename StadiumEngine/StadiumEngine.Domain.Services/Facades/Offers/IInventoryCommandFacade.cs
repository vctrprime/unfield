#nullable enable
using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.Common.Models;
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Services.Facades.Offers;

public interface IInventoryCommandFacade
{
    Task AddInventoryAsync( Inventory inventory, List<ImageFile> images, int legalId );
    Task UpdateInventoryAsync( Inventory inventory, List<ImageFile> images, List<SportKind> sportKinds );
    Task DeleteInventoryAsync( int inventoryId, int stadiumId );
}