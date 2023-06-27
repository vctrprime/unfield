using Mediator;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Commands.Accounts.Users;

public sealed class ChangeStadiumCommand : IRequest<AuthorizeUserDto?>
{
    public int StadiumId { get; set; }
}