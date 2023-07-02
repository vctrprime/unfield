using Mediator;
using StadiumEngine.DTO.Accounts.Roles;

namespace StadiumEngine.Commands.Accounts.Roles;

public sealed class UpdateRoleCommand : BaseCommand, IRequest<UpdateRoleDto>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}