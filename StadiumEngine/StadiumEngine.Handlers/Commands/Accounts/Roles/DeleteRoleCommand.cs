using Mediator;
using StadiumEngine.DTO.Accounts.Roles;

namespace StadiumEngine.Handlers.Commands.Accounts.Roles;

public sealed class DeleteRoleCommand : IRequest<DeleteRoleDto>
{
    public DeleteRoleCommand(int roleId)
    {
        RoleId = roleId;
    }

    public int RoleId { get; }
}