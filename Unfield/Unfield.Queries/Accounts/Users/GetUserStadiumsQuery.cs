using Mediator;
using Unfield.DTO.Accounts.Users;

namespace Unfield.Queries.Accounts.Users;

public sealed class GetUserStadiumsQuery : BaseQuery, IRequest<List<UserStadiumDto>>
{
}