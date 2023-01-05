using Mediator;
using StadiumEngine.DTO.Accounts;

namespace StadiumEngine.Handlers.Commands.Accounts;

public sealed class AddRoleCommand : IRequest<AddRoleDto>
{
    public string Name { get; set; }
    public string? Description { get; set; }
}