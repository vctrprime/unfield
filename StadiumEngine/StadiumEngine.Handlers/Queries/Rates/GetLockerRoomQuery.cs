using Mediator;
using StadiumEngine.DTO.Rates.PriceGroups;

namespace StadiumEngine.Handlers.Queries.Rates;

public sealed class GetPriceGroupQuery : IRequest<PriceGroupDto>
{
    public GetPriceGroupQuery(int priceGroupId)
    {
        PriceGroupId = priceGroupId;
    }
    
    public int PriceGroupId { get; }
}