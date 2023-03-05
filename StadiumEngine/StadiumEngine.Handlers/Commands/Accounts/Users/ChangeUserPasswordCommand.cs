using Mediator;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Handlers.Commands.Accounts.Users;

public class ChangeUserPasswordCommand : IRequest<ChangeUserPasswordDto>
{
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string NewPasswordRepeat { get; set; } = null!;
}