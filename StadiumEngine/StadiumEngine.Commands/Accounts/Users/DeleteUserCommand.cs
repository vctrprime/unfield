using Mediator;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Commands.Accounts.Users;

public sealed class DeleteUserCommand : BaseCommand, IRequest<DeleteUserDto>
{
    public int UserId { get; set; }
}