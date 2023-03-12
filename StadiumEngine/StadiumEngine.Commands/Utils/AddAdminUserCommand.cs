using Mediator;
using StadiumEngine.DTO.Utils;

namespace StadiumEngine.Commands.Utils;

public class AddAdminUserCommand : IRequest<AddAdminUserDto>
{
    public string Name { get; set; } = null!;
    public string? LastName { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public string? Description { get; set; }
}