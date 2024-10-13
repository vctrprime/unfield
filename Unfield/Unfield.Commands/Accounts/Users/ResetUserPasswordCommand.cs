using Mediator;
using Unfield.DTO.Accounts.Users;

namespace Unfield.Commands.Accounts.Users;

public sealed class ResetUserPasswordCommand : BaseCommand, IRequest<ResetUserPasswordDto>
{
    public string PhoneNumber { get; set; } = null!;
}