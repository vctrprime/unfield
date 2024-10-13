using Mediator;
using Unfield.DTO.Accounts.Users;

namespace Unfield.Commands.Accounts.Users;

public sealed class DeleteUserCommand : BaseCommand, IRequest<DeleteUserDto>
{
    public int UserId { get; set; }
}