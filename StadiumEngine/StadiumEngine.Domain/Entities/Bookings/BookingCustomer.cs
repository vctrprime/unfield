namespace StadiumEngine.Domain.Entities.Bookings;

public class BookingCustomer : BaseUserEntity
{
    public int BookingId { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    
    public virtual Booking Booking { get; set; }
}