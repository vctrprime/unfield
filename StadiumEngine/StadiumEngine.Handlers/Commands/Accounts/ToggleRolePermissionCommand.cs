using Mediator;
using StadiumEngine.DTO.Accounts;

namespace StadiumEngine.Handlers.Commands.Accounts;

public class ToggleRolePermissionCommand : IRequest<ToggleRolePermissionDto>
{
    public int RoleId { get; set; }
    public int PermissionId { get; set; }
}