using Mediator;
using StadiumEngine.DTO.Accounts;

namespace StadiumEngine.Handlers.Commands.Accounts;

public sealed class UpdateRoleCommand : IRequest<UpdateRoleDto>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}