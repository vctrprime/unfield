using Unfield.Domain.Entities.Bookings;

namespace Unfield.Domain.Services.Models.Schedule;

public class BookingListItem
{
    public DateTime? Day { get; set; }
    public DateTime? ClosedDay { get; set; }
    public Booking OriginalData { get; set; } = null!;
}