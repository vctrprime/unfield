using Mediator;
using StadiumEngine.DTO.Accounts.Roles;

namespace StadiumEngine.Commands.Accounts.Roles;

public sealed class AddRoleCommand : IRequest<AddRoleDto>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}