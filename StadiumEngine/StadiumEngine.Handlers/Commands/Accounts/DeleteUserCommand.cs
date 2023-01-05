using Mediator;
using StadiumEngine.DTO.Accounts;

namespace StadiumEngine.Handlers.Commands.Accounts;

public sealed class DeleteUserCommand : IRequest<DeleteUserDto>
{
    public DeleteUserCommand(int userId)
    {
        UserId = userId;
    }

    public int UserId { get; }
}