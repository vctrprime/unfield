using Mediator;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Customers;

namespace StadiumEngine.Commands.Customers;

public sealed class CancelBookingByCustomerCommand : BaseCancelBookingCommand, IRequest<CancelBookingByCustomerDto>
{
    [FromHeader( Name = "SE-Stadium-Token" )]
    public string StadiumToken { get; set; } = null!;
}