using Mediator;
using StadiumEngine.DTO.Accounts.Permissions;

namespace StadiumEngine.Handlers.Queries.Accounts.Roles;

public sealed class GetPermissionsForRoleQuery : IRequest<List<PermissionDto>>
{
    public GetPermissionsForRoleQuery(int roleId)
    {
        RoleId = roleId;
    }

    public int RoleId { get;}
}