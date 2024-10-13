using Mediator;
using Unfield.DTO.Accounts.Roles;

namespace Unfield.Commands.Accounts.Roles;

public sealed class AddRoleCommand : BaseCommand, IRequest<AddRoleDto>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}