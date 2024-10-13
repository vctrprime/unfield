using Mediator;
using Unfield.DTO.Accounts.Stadiums;

namespace Unfield.Queries.Accounts.Users;

public sealed class GetStadiumsForUserQuery : BaseQuery, IRequest<List<StadiumDto>>
{
    public int UserId { get; set; }
}