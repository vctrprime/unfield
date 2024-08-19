using Mediator;

namespace StadiumEngine.Commands.EventBus.Bookings;

public class BookingConfirmedCommand : IRequest<bool>
{
    public string BookingNumber { get; set; }
}