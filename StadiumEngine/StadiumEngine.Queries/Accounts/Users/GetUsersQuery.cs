using Mediator;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Queries.Accounts.Users;

public sealed class GetUsersQuery : BaseQuery, IRequest<List<UserDto>>
{
}