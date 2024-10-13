using Mediator;
using Microsoft.AspNetCore.Mvc;
using Unfield.DTO.Customers;

namespace Unfield.Commands.Customers;

public class ChangeCustomerPasswordCommand : BaseCustomerCommand, IRequest<ChangeCustomerPasswordDto>
{
    [FromBody]
    public ChangeCustomerPasswordCommandBody Data { get; set; } = null!;
}

public class ChangeCustomerPasswordCommandBody
{
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string NewPasswordRepeat { get; set; } = null!;
}