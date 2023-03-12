using Mediator;
using StadiumEngine.DTO.Accounts.Roles;

namespace StadiumEngine.Commands.Accounts.Roles;

public class ToggleRoleStadiumCommand : IRequest<ToggleRoleStadiumDto>
{
    public int RoleId { get; set; }
    public int StadiumId { get; set; }
}