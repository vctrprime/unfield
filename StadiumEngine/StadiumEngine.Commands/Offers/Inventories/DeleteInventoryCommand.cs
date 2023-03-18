using Mediator;
using StadiumEngine.DTO.Offers.Inventories;

namespace StadiumEngine.Commands.Offers.Inventories;

public sealed class DeleteInventoryCommand : IRequest<DeleteInventoryDto>
{
    public DeleteInventoryCommand( int inventoryId )
    {
        InventoryId = inventoryId;
    }

    public int InventoryId { get; }
}