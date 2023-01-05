using Mediator;
using StadiumEngine.DTO.Accounts;

namespace StadiumEngine.Handlers.Commands.Accounts;

public class ToggleRoleStadiumCommand : IRequest<ToggleRoleStadiumDto>
{
    public int RoleId { get; set; }
    public int StadiumId { get; set; }
}