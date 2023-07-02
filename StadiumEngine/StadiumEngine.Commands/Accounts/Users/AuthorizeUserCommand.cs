using Mediator;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Commands.Accounts.Users;

public sealed class AuthorizeUserCommand : BaseCommand, IRequest<AuthorizeUserDto?>
{
    public string? Login { get; set; }
    public string? Password { get; set; }
}