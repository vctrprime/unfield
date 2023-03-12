using Mediator;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Queries.Accounts.Users;

public sealed class GetUserPermissionsQuery : IRequest<List<UserPermissionDto>>
{
}