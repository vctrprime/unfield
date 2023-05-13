using Mediator;
using StadiumEngine.DTO.Accounts.Stadiums;

namespace StadiumEngine.Queries.Accounts.Roles;

public sealed class GetStadiumsForRoleQuery : IRequest<List<StadiumDto>>
{
    public int RoleId { get; set; }
}