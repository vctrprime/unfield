using Mediator;
using Unfield.DTO.Accounts.Roles;

namespace Unfield.Queries.Accounts.Roles;

public sealed class GetRolesQuery : BaseQuery, IRequest<List<RoleDto>>
{
}