using Mediator;
using Unfield.DTO.Offers.Inventories;

namespace Unfield.Queries.Offers.Inventories;

public sealed class GetInventoryQuery : BaseQuery, IRequest<InventoryDto>
{
    public int InventoryId { get; set; }
}