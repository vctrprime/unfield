using Mediator;
using StadiumEngine.DTO.Accounts.Roles;

namespace StadiumEngine.Queries.Accounts.Roles;

public sealed class GetRolesQuery : IRequest<List<RoleDto>>
{
}