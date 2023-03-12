using Mediator;
using StadiumEngine.DTO.Offers.Inventories;

namespace StadiumEngine.Queries.Offers.Inventories;

public sealed class GetInventoriesQuery : IRequest<List<InventoryDto>>
{
}