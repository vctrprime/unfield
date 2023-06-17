using System.ComponentModel.DataAnnotations.Schema;

namespace StadiumEngine.Domain.Entities.Bookings;

[Table( "booking_customer", Schema = "bookings" )]
public class BookingCustomer : BaseBookingEntity
{
    [Column( "booking_id" )]
    public int BookingId { get; set; }
    
    [Column( "name" )]
    public new string Name { get; set; }
    
    [Column( "phone_number" )]
    public string PhoneNumber { get; set; }
    
    [ForeignKey( "BookingId" )]
    public virtual Booking Booking { get; set; }
}