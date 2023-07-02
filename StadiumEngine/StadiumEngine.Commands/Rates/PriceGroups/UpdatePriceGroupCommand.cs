using Mediator;
using StadiumEngine.DTO.Rates.PriceGroups;

namespace StadiumEngine.Commands.Rates.PriceGroups;

public sealed class UpdatePriceGroupCommand : BaseCommand, IRequest<UpdatePriceGroupDto>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}