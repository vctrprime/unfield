using Mediator;
using Unfield.DTO.Rates.PriceGroups;

namespace Unfield.Commands.Rates.PriceGroups;

public sealed class DeletePriceGroupCommand : BaseCommand, IRequest<DeletePriceGroupDto>
{
    public int PriceGroupId { get; set; }
}