using Mediator;
using StadiumEngine.DTO.Rates.PriceGroups;

namespace StadiumEngine.Queries.Rates.PriceGroups;

public sealed class GetPriceGroupQuery : IRequest<PriceGroupDto>
{
    public GetPriceGroupQuery( int priceGroupId )
    {
        PriceGroupId = priceGroupId;
    }

    public int PriceGroupId { get; }
}