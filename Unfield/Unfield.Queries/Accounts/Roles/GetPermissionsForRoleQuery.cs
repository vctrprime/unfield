using Mediator;
using Unfield.DTO.Accounts.Permissions;

namespace Unfield.Queries.Accounts.Roles;

public sealed class GetPermissionsForRoleQuery : BaseQuery, IRequest<List<PermissionDto>>
{
    public int RoleId { get; set; }
}