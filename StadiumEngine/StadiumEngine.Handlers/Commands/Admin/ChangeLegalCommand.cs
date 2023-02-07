using Mediator;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Handlers.Commands.Admin;

public sealed class ChangeLegalCommand: IRequest<AuthorizeUserDto?>
{
    public int LegalId { get; }

    public ChangeLegalCommand(int legalId)
    {
        LegalId = legalId;
    }
}