using Mediator;
using StadiumEngine.DTO.Accounts;

namespace StadiumEngine.Handlers.Queries.Accounts;

public sealed class GetPermissionsForRoleQuery : IRequest<List<PermissionDto>>
{
    public GetPermissionsForRoleQuery(int roleId)
    {
        RoleId = roleId;
    }

    public int RoleId { get;}
}