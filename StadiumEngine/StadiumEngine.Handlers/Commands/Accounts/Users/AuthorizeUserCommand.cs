using Mediator;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Handlers.Commands.Accounts.Users;

public sealed class AuthorizeUserCommand : IRequest<AuthorizeUserDto?>
{
    public string? Login { get; set; }
    public string? Password { get; set; }
}