using Mediator;
using Microsoft.AspNetCore.Mvc;
using Unfield.DTO.Customers;

namespace Unfield.Commands.Customers;

public class AuthorizeCustomerCommand : BaseCustomerCommand, IRequest<AuthorizeCustomerDto>
{
    [FromBody]
    public AuthorizeCustomerCommandBody Data { get; set; } = null!;
}

public class AuthorizeCustomerCommandBody 
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
}