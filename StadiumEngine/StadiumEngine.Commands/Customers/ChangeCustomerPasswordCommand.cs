using Mediator;
using StadiumEngine.DTO.Customers;

namespace StadiumEngine.Commands.Customers;

public class ChangeCustomerPasswordCommand : BaseCustomerCommand, IRequest<ChangeCustomerPasswordDto>
{
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string NewPasswordRepeat { get; set; } = null!;
}