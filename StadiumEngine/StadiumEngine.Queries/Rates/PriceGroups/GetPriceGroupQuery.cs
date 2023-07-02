using Mediator;
using StadiumEngine.DTO.Rates.PriceGroups;

namespace StadiumEngine.Queries.Rates.PriceGroups;

public sealed class GetPriceGroupQuery : BaseQuery, IRequest<PriceGroupDto>
{
    public int PriceGroupId { get; set; }
}