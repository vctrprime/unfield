using Mediator;
using StadiumEngine.DTO.Accounts;

namespace StadiumEngine.Handlers.Commands.Accounts;

public sealed class DeleteRoleCommand : IRequest<DeleteRoleDto>
{
    public DeleteRoleCommand(int roleId)
    {
        RoleId = roleId;
    }

    public int RoleId { get; }
}