using Mediator;
using Unfield.DTO.Accounts.Users;

namespace Unfield.Commands.Accounts.Users;

public class AddUserCommand : BaseCommand, IRequest<AddUserDto>
{
    public string Name { get; set; } = null!;
    public string? LastName { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public int RoleId { get; set; }
    public string? Description { get; set; }
}