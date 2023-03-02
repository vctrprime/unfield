using Mediator;
using StadiumEngine.DTO.Rates.PriceGroups;

namespace StadiumEngine.Handlers.Commands.Rates.PriceGroups;

public sealed class AddPriceGroupCommand : IRequest<AddPriceGroupDto>
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}