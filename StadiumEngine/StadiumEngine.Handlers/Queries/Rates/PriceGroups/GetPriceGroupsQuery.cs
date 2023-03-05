using Mediator;
using StadiumEngine.DTO.Rates.PriceGroups;

namespace StadiumEngine.Handlers.Queries.Rates.PriceGroups;

public sealed class GetPriceGroupsQuery : IRequest<List<PriceGroupDto>>
{
}