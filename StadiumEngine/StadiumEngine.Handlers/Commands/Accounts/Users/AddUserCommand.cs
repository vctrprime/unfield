using Mediator;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Handlers.Commands.Accounts.Users;

public class AddUserCommand : IRequest<AddUserDto>
{
    public string Name { get; set; }
    public string? LastName { get; set; }
    public string PhoneNumber { get; set; }
    public int RoleId { get; set; }
    public string? Description { get; set; }
}