using Mediator;
using StadiumEngine.DTO.Accounts;

namespace StadiumEngine.Handlers.Commands.Accounts;

public class AuthorizeUserCommand : IRequest<AuthorizeUserDto?>
{
    public string Login { get; set; }
    public string Password { get; set; }
}