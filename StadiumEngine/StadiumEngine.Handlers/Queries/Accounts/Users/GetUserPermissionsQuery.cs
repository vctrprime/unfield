using Mediator;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Handlers.Queries.Accounts.Users;

public sealed class GetUserPermissionsQuery : IRequest<List<UserPermissionDto>>
{
}