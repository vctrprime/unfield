using Mediator;
using Unfield.DTO.Accounts.Users;

namespace Unfield.Queries.Accounts.Users;

public sealed class GetUsersQuery : BaseQuery, IRequest<List<UserDto>>
{
}