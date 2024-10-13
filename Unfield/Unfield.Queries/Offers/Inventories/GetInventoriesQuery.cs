using Mediator;
using Unfield.DTO.Offers.Inventories;

namespace Unfield.Queries.Offers.Inventories;

public sealed class GetInventoriesQuery : BaseQuery, IRequest<List<InventoryDto>>
{
}