using Mediator;
using StadiumEngine.DTO.Accounts.Roles;

namespace StadiumEngine.Commands.Accounts.Roles;

public sealed class DeleteRoleCommand : IRequest<DeleteRoleDto>
{
    public int RoleId { get; set; }
}