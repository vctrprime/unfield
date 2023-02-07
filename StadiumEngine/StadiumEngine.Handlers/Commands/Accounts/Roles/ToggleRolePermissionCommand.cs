using Mediator;
using StadiumEngine.DTO.Accounts.Roles;

namespace StadiumEngine.Handlers.Commands.Accounts.Roles;

public class ToggleRolePermissionCommand : IRequest<ToggleRolePermissionDto>
{
    public int RoleId { get; set; }
    public int PermissionId { get; set; }
}