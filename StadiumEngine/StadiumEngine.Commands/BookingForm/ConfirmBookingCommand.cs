using Mediator;
using StadiumEngine.DTO.BookingForm;

namespace StadiumEngine.Commands.BookingForm;

public sealed class ConfirmBookingCommand : IRequest<ConfirmBookingDto>
{
    public string BookingNumber { get; set; } = null!;
    public string AccessCode { get; set; } = null!;
}