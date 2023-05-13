using Mediator;
using StadiumEngine.DTO.Accounts.Permissions;

namespace StadiumEngine.Queries.Accounts.Roles;

public sealed class GetPermissionsForRoleQuery : IRequest<List<PermissionDto>>
{
    public int RoleId { get; set; }
}