using Mediator;
using StadiumEngine.DTO.Accounts.Stadiums;

namespace StadiumEngine.Handlers.Queries.Accounts.Roles;

public sealed class GetStadiumsForRoleQuery : IRequest<List<StadiumDto>>
{
    public GetStadiumsForRoleQuery(int roleId)
    {
        RoleId = roleId;
    }

    public int RoleId { get; }
}