using System.ComponentModel.DataAnnotations.Schema;

namespace StadiumEngine.Domain.Entities.BookingForm;

[Table( "booking_cost", Schema = "bookings" )]
public class BookingCost : BaseBookingEntity
{
    [Column( "booking_id" )]
    public int BookingId { get; set; }
    
    [Column( "start_hour" )]
    public decimal StartHour { get; set; }
    
    [Column( "end_hour" )]
    public decimal EndHour { get; set; }
    
    [Column( "cost" )]
    public decimal Cost { get; set; }
    
    [ForeignKey( "BookingId" )]
    public virtual Booking Booking { get; set; }
}