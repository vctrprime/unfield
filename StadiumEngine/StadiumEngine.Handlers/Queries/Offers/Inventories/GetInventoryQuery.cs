using Mediator;
using StadiumEngine.DTO.Offers.Inventories;

namespace StadiumEngine.Handlers.Queries.Offers.Inventories;

public sealed class GetInventoryQuery : IRequest<InventoryDto>
{
    public GetInventoryQuery( int inventoryId )
    {
        InventoryId = inventoryId;
    }

    public int InventoryId { get; }
}