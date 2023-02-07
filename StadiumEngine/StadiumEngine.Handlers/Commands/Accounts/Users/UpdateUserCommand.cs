using Mediator;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Handlers.Commands.Accounts.Users;

public sealed class UpdateUserCommand : IRequest<UpdateUserDto>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? LastName { get; set; }
    public int RoleId { get; set; }
    public string? Description { get; set; }
}