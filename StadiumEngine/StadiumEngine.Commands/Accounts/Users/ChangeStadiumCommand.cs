using Mediator;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Commands.Accounts.Users;

public sealed class ChangeStadiumCommand : BaseCommand, IRequest<AuthorizeUserDto?>
{
    public int StadiumId { get; set; }
}