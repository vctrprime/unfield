using Mediator;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Customers;

namespace StadiumEngine.Commands.Customers;

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