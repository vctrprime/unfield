using Mediator;
using StadiumEngine.DTO.Customers;

namespace StadiumEngine.Commands.Customers;

public sealed class ResetCustomerPasswordCommand : BaseCustomerCommand, IRequest<ResetCustomerPasswordDto>
{
    public string PhoneNumber { get; set; } = null!;
}