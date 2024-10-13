using Mediator;
using Unfield.DTO.Accounts.Roles;

namespace Unfield.Commands.Accounts.Roles;

public class ToggleRolePermissionCommand : BaseCommand, IRequest<ToggleRolePermissionDto>
{
    public int RoleId { get; set; }
    public int PermissionId { get; set; }
}