using Mediator;
using StadiumEngine.DTO.Offers.Inventories;

namespace StadiumEngine.Queries.Offers.Inventories;

public sealed class GetInventoryQuery : BaseQuery, IRequest<InventoryDto>
{
    public int InventoryId { get; set; }
}