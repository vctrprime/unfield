using Mediator;
using Unfield.DTO.Rates.PriceGroups;

namespace Unfield.Queries.Rates.PriceGroups;

public sealed class GetPriceGroupQuery : BaseQuery, IRequest<PriceGroupDto>
{
    public int PriceGroupId { get; set; }
}