using Mediator;
using StadiumEngine.DTO.Accounts.Roles;

namespace StadiumEngine.Handlers.Commands.Accounts.Roles;

public sealed class AddRoleCommand : IRequest<AddRoleDto>
{
    public string Name { get; set; }
    public string? Description { get; set; }
}