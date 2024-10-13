using Mediator;
using Unfield.DTO.Accounts.Users;

namespace Unfield.Commands.Accounts.Users;

public sealed class ChangeStadiumCommand : BaseCommand, IRequest<AuthorizeUserDto?>
{
    public int StadiumId { get; set; }
}