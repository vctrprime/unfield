#nullable enable
using Unfield.Common.Enums.Offers;
using Unfield.Common.Models;
using Unfield.Domain.Entities.Offers;

namespace Unfield.Domain.Services.Core.Offers;

public interface IInventoryCommandService
{
    Task AddInventoryAsync( Inventory inventory, List<ImageFile> images, int stadiumGroupId );
    Task UpdateInventoryAsync( Inventory inventory, List<ImageFile> images, List<SportKind> sportKinds );
    Task DeleteInventoryAsync( int inventoryId, int stadiumId );
}