using Mediator;
using StadiumEngine.DTO.Rates.PriceGroups;

namespace StadiumEngine.Queries.Rates.PriceGroups;

public sealed class GetPriceGroupsQuery : BaseQuery, IRequest<List<PriceGroupDto>>
{
}