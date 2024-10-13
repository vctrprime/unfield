using Mediator;

namespace Unfield.Commands.EventBus.Bookings;

public class BookingConfirmedCommand : IRequest<bool>
{
    public string BookingNumber { get; set; }
}