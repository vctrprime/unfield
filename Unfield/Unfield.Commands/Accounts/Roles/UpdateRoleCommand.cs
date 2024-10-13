using Mediator;
using Unfield.DTO.Accounts.Roles;

namespace Unfield.Commands.Accounts.Roles;

public sealed class UpdateRoleCommand : BaseCommand, IRequest<UpdateRoleDto>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}