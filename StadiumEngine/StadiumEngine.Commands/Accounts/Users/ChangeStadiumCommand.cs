using Mediator;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Commands.Accounts.Users;

public sealed class ChangeStadiumCommand : IRequest<AuthorizeUserDto?>
{
    public ChangeStadiumCommand( int stadiumId )
    {
        StadiumId = stadiumId;
    }

    public int StadiumId { get; }
}