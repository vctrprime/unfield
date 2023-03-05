using Mediator;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Handlers.Commands.Accounts.Users;

public sealed class ChangeStadiumCommand : IRequest<AuthorizeUserDto?>
{
    public ChangeStadiumCommand( int stadiumId )
    {
        StadiumId = stadiumId;
    }

    public int StadiumId { get; }
}