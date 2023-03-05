using Mediator;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Handlers.Commands.Accounts.Users;

public sealed class ResetUserPasswordCommand : IRequest<ResetUserPasswordDto>
{
    public string PhoneNumber { get; set; } = null!;
}