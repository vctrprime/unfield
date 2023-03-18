using Mediator;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Commands.Admin;

public sealed class ChangeLegalCommand : IRequest<AuthorizeUserDto?>
{
    public ChangeLegalCommand( int legalId )
    {
        LegalId = legalId;
    }

    public int LegalId { get; }
}