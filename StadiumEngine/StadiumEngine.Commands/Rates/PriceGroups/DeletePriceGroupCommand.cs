using Mediator;
using StadiumEngine.DTO.Rates.PriceGroups;

namespace StadiumEngine.Commands.Rates.PriceGroups;

public sealed class DeletePriceGroupCommand : IRequest<DeletePriceGroupDto>
{
    public DeletePriceGroupCommand( int priceGroupId )
    {
        PriceGroupId = priceGroupId;
    }

    public int PriceGroupId { get; }
}