using Mediator;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Queries.Accounts.Users;

public sealed class GetAuthorizedUserQuery : BaseQuery, IRequest<AuthorizedUserDto>
{
}