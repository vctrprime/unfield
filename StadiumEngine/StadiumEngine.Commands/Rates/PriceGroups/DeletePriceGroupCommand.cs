using Mediator;
using StadiumEngine.DTO.Rates.PriceGroups;

namespace StadiumEngine.Commands.Rates.PriceGroups;

public sealed class DeletePriceGroupCommand : BaseCommand, IRequest<DeletePriceGroupDto>
{
    public int PriceGroupId { get; set; }
}