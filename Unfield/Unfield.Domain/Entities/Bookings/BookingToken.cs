using Unfield.Common.Enums.Bookings;

namespace Unfield.Domain.Entities.Bookings;

public class BookingToken : BaseEntity
{
    public int BookingId { get; set; }
    public string Token { get; set; }
    public BookingTokenType Type { get; set; }
    
    public virtual Booking Booking { get; set; }
}