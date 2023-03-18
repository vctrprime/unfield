using Mediator;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Commands.Accounts.Users;

public sealed class DeleteUserCommand : IRequest<DeleteUserDto>
{
    public DeleteUserCommand( int userId )
    {
        UserId = userId;
    }

    public int UserId { get; }
}