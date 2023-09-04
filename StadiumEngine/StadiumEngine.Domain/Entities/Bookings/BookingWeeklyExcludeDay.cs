using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StadiumEngine.Domain.Entities.Bookings;

[Table( "booking_weekly_exclude_day", Schema = "bookings" )]
public class BookingWeeklyExcludeDay : BaseBookingEntity
{
    [Column( "booking_id" )]
    public int BookingId { get; set; }
    
    [Column( "day", TypeName = "timestamp without time zone")]
    public DateTime Day { get; set; }
    
    [ForeignKey( "BookingId" )]
    public virtual Booking Booking { get; set; }
}