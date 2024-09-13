using Mediator;
using StadiumEngine.DTO.Customers;

namespace StadiumEngine.Commands.Customers;

public class AuthorizeCustomerCommand : BaseCustomerCommand, IRequest<AuthorizeCustomerDto>
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
}