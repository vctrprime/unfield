using Mediator;
using Unfield.DTO.Rates.PriceGroups;

namespace Unfield.Commands.Rates.PriceGroups;

public sealed class AddPriceGroupCommand : BaseCommand, IRequest<AddPriceGroupDto>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}