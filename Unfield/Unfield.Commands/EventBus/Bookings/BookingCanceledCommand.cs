using Mediator;

namespace Unfield.Commands.EventBus.Bookings;

public class BookingCanceledCommand : IRequest<bool>
{
    public string BookingNumber { get; set; } = null!;
    public DateTime Day { get; set; }
    public string? Reason { get; set; }
}