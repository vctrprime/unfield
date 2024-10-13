using Mediator;
using Unfield.DTO.Accounts.Users;

namespace Unfield.Queries.Accounts.Users;

public sealed class GetAuthorizedUserQuery : BaseQuery, IRequest<AuthorizedUserDto>
{
}