using Mediator;
using Unfield.DTO.Rates.PriceGroups;

namespace Unfield.Commands.Rates.PriceGroups;

public sealed class UpdatePriceGroupCommand : BaseCommand, IRequest<UpdatePriceGroupDto>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}