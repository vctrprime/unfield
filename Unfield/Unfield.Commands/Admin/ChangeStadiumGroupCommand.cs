using Mediator;
using Unfield.DTO.Accounts.Users;

namespace Unfield.Commands.Admin;

public sealed class ChangeStadiumGroupCommand : BaseCommand, IRequest<AuthorizeUserDto?>
{
    public int StadiumGroupId { get; set; }
}