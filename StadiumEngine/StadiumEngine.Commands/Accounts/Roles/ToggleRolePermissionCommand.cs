using Mediator;
using StadiumEngine.DTO.Accounts.Roles;

namespace StadiumEngine.Commands.Accounts.Roles;

public class ToggleRolePermissionCommand : BaseCommand, IRequest<ToggleRolePermissionDto>
{
    public int RoleId { get; set; }
    public int PermissionId { get; set; }
}