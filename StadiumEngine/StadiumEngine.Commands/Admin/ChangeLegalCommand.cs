using Mediator;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Commands.Admin;

public sealed class ChangeLegalCommand : BaseCommand, IRequest<AuthorizeUserDto?>
{
    public int LegalId { get; set; }
}