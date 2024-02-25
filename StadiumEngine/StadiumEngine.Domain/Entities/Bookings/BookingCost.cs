namespace StadiumEngine.Domain.Entities.Bookings;

public class BookingCost : BaseUserEntity
{
    public int BookingId { get; set; }
    public decimal StartHour { get; set; }
    public decimal EndHour { get; set; }
    public decimal Cost { get; set; }
    
    public virtual Booking Booking { get; set; }
}