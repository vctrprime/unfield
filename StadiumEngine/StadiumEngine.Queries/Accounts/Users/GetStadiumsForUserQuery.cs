using Mediator;
using StadiumEngine.DTO.Accounts.Stadiums;

namespace StadiumEngine.Queries.Accounts.Users;

public sealed class GetStadiumsForUserQuery : BaseQuery, IRequest<List<StadiumDto>>
{
    public int UserId { get; set; }
}