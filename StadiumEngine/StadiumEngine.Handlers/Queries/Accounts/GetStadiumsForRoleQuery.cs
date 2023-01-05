using Mediator;
using StadiumEngine.DTO.Accounts;

namespace StadiumEngine.Handlers.Queries.Accounts;

public sealed class GetStadiumsForRoleQuery : IRequest<List<StadiumDto>>
{
    public GetStadiumsForRoleQuery(int roleId)
    {
        RoleId = roleId;
    }

    public int RoleId { get; }
}