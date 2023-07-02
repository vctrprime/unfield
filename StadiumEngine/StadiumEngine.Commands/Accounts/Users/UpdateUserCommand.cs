using Mediator;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Commands.Accounts.Users;

public sealed class UpdateUserCommand : BaseCommand, IRequest<UpdateUserDto>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? LastName { get; set; }
    public int RoleId { get; set; }
    public string? Description { get; set; }
}