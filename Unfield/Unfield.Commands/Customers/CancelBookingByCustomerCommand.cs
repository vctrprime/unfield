using Mediator;
using Microsoft.AspNetCore.Mvc;
using Unfield.DTO.Customers;

namespace Unfield.Commands.Customers;

public sealed class CancelBookingByCustomerCommand : BaseCancelBookingCommand, IRequest<CancelBookingByCustomerDto>
{
    [FromHeader( Name = "SE-Stadium-Token" )]
    public string StadiumToken { get; set; } = null!;
}