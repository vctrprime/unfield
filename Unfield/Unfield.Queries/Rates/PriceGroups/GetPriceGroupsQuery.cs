using Mediator;
using Unfield.DTO.Rates.PriceGroups;

namespace Unfield.Queries.Rates.PriceGroups;

public sealed class GetPriceGroupsQuery : BaseQuery, IRequest<List<PriceGroupDto>>
{
}