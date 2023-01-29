using Mediator;
using StadiumEngine.DTO.Utils;

namespace StadiumEngine.Handlers.Commands.Utils;

public class AddAdminUserCommand : IRequest<AddAdminUserDto>
{
    public string Name { get; set; }
    public string? LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string? Description { get; set; }
}