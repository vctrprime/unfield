using Mediator;
using StadiumEngine.DTO.Accounts;

namespace StadiumEngine.Handlers.Commands.Accounts;

public sealed class ResetUserPasswordCommand : IRequest<ResetUserPasswordDto>
{
    public string PhoneNumber { get; set; }
}