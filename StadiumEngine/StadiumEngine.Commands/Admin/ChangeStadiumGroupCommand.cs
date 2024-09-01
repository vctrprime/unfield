using Mediator;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Commands.Admin;

public sealed class ChangeStadiumGroupCommand : BaseCommand, IRequest<AuthorizeUserDto?>
{
    public int StadiumGroupId { get; set; }
}