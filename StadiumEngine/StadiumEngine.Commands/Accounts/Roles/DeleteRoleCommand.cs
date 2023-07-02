using Mediator;
using StadiumEngine.DTO.Accounts.Roles;

namespace StadiumEngine.Commands.Accounts.Roles;

public sealed class DeleteRoleCommand : BaseCommand, IRequest<DeleteRoleDto>
{
    public int RoleId { get; set; }
}