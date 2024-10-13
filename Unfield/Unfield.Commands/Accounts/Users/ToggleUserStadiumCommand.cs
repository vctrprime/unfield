using Mediator;
using Unfield.DTO.Accounts.Users;

namespace Unfield.Commands.Accounts.Users;

public class ToggleUserStadiumCommand : BaseCommand, IRequest<ToggleUserStadiumDto>
{
    public int UserId { get; set; }
    public int StadiumId { get; set; }
}