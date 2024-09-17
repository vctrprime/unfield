using Mediator;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Customers;

namespace StadiumEngine.Commands.Customers;

public class RegisterCustomerCommand : BaseCustomerCommand, IRequest<RegisterCustomerDto>
{
    [FromBody]
    public RegisterCustomerCommandBody Data { get; set; } = null!;
}

public class RegisterCustomerCommandBody
{
    public string PhoneNumber { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Language { get; set; } = "ru";
}