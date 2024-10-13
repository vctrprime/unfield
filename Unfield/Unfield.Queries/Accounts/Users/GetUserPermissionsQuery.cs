using Mediator;
using Unfield.DTO.Accounts.Users;

namespace Unfield.Queries.Accounts.Users;

public sealed class GetUserPermissionsQuery : BaseQuery, IRequest<List<UserPermissionDto>>
{
}