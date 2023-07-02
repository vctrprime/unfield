using Mediator;
using StadiumEngine.DTO.BookingForm;

namespace StadiumEngine.Commands.BookingForm;

public sealed class CancelBookingCommand : BaseCommand, IRequest<CancelBookingDto>
{
    public string BookingNumber { get; set; } = null!;
}