using Mediator;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Handlers.Queries.Accounts.Users;

public sealed class GetUsersQuery : IRequest<List<UserDto>>
{
}