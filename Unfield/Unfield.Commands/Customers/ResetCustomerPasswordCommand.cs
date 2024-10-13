using Mediator;
using Microsoft.AspNetCore.Mvc;
using Unfield.DTO.Customers;

namespace Unfield.Commands.Customers;

public sealed class ResetCustomerPasswordCommand : BaseCustomerCommand, IRequest<ResetCustomerPasswordDto>
{
    [FromBody]
    public ResetCustomerPasswordCommandBody Data { get; set; } = null!;
}

public class ResetCustomerPasswordCommandBody
{
    public string PhoneNumber { get; set; } = null!;
}