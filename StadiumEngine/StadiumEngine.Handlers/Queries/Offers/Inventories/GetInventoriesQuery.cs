using Mediator;
using StadiumEngine.DTO.Offers.Inventories;

namespace StadiumEngine.Handlers.Queries.Offers.Inventories;

public sealed class GetInventoriesQuery : IRequest<List<InventoryDto>>
{
}