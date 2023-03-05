using Mediator;
using StadiumEngine.DTO.Rates.PriceGroups;

namespace StadiumEngine.Handlers.Queries.Rates;

public sealed class GetPriceGroupsQuery : IRequest<List<PriceGroupDto>>
{
}