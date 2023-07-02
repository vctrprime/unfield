using Mediator;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Commands.Accounts.Users;

public class ChangeUserPasswordCommand : BaseCommand, IRequest<ChangeUserPasswordDto>
{
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string NewPasswordRepeat { get; set; } = null!;
}