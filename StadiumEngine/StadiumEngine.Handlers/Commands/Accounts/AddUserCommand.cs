using Mediator;
using StadiumEngine.DTO.Accounts;

namespace StadiumEngine.Handlers.Commands.Accounts;

public sealed class AddUserCommand : IRequest<AddUserDto>
{
    public string Name { get; set; }
    public string? LastName { get; set; }
    public string PhoneNumber { get; set; }
    public int RoleId { get; set; }
    public string? Description { get; set; }
}