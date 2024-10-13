using Mediator;
using Microsoft.AspNetCore.Mvc;
using Unfield.DTO.Customers;

namespace Unfield.Commands.Customers;

public sealed class UpdateCustomerCommand : BaseCustomerCommand, IRequest<AuthorizedCustomerDto>
{
    [FromBody]
    public UpdateCustomerCommandBody Data { get; set; } = null!;
}

public class UpdateCustomerCommandBody
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}