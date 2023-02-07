using Mediator;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Handlers.Commands.Accounts.Users;

public class ChangeUserPasswordCommand : IRequest<ChangeUserPasswordDto>
{
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public string NewPasswordRepeat { get; set; }
}