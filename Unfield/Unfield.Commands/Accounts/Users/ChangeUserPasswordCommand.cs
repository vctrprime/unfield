using Mediator;
using Unfield.DTO.Accounts.Users;

namespace Unfield.Commands.Accounts.Users;

public class ChangeUserPasswordCommand : BaseCommand, IRequest<ChangeUserPasswordDto>
{
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string NewPasswordRepeat { get; set; } = null!;
}