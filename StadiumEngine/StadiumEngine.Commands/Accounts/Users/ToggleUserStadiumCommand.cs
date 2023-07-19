using Mediator;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Commands.Accounts.Users;

public class ToggleUserStadiumCommand : BaseCommand, IRequest<ToggleUserStadiumDto>
{
    public int UserId { get; set; }
    public int StadiumId { get; set; }
}