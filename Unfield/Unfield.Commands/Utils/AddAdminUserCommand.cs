using Mediator;
using Unfield.DTO.Utils;

namespace Unfield.Commands.Utils;

public class AddAdminUserCommand : BaseCommand, IRequest<AddAdminUserDto>
{
    public string Name { get; set; } = null!;
    public string? LastName { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public string? Description { get; set; }
}