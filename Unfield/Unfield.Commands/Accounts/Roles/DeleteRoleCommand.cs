using Mediator;
using Unfield.DTO.Accounts.Roles;

namespace Unfield.Commands.Accounts.Roles;

public sealed class DeleteRoleCommand : BaseCommand, IRequest<DeleteRoleDto>
{
    public int RoleId { get; set; }
}