using Mediator;
using StadiumEngine.DTO.Accounts.Roles;

namespace StadiumEngine.Handlers.Commands.Accounts.Roles;

public sealed class UpdateRoleCommand : IRequest<UpdateRoleDto>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}