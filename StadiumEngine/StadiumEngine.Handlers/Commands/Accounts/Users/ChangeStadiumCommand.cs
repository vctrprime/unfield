using Mediator;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Handlers.Commands.Accounts.Users;

public sealed class ChangeStadiumCommand : IRequest<AuthorizeUserDto?>
{
    public int StadiumId { get; }

    public ChangeStadiumCommand(int stadiumId)
    {
        StadiumId = stadiumId;
    }
}