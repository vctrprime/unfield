using Mediator;
using Unfield.DTO.Accounts.Users;

namespace Unfield.Commands.Accounts.Users;

public sealed class AuthorizeUserCommand : BaseCommand, IRequest<AuthorizeUserDto?>
{
    public string? Login { get; set; }
    public string? Password { get; set; }
}