using Mediator;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Customers;

namespace StadiumEngine.Commands.Customers;

public sealed class UpdateCustomerCommand : BaseCustomerCommand, IRequest<UpdateCustomerDto>
{
    [FromBody]
    public UpdateCustomerCommandBody Data { get; set; } = null!;
}

public class UpdateCustomerCommandBody
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}