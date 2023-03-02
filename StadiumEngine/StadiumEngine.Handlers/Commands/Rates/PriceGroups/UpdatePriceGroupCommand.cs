using Mediator;
using StadiumEngine.DTO.Rates.PriceGroups;

namespace StadiumEngine.Handlers.Commands.Rates.PriceGroups;

public sealed class UpdatePriceGroupCommand : IRequest<UpdatePriceGroupDto>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}