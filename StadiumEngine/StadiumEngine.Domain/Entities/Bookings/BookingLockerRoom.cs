using System;
using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Entities.Bookings;

[Table( "booking_locker_room", Schema = "bookings" )]
public class BookingLockerRoom : BaseBookingEntity
{
    [Column( "booking_id" )]
    public int BookingId { get; set; }
    
    [Column( "locker_room_id" )]
    public int LockerRoomId { get; set; }
    
    [Column( "start", TypeName = "timestamp without time zone")]
    public DateTime Start { get; set; }
    
    [Column( "end", TypeName = "timestamp without time zone")]
    public DateTime End { get; set; }
    
    [ForeignKey( "BookingId" )]
    public virtual Booking Booking { get; set; }
    
    [ForeignKey( "LockerRoomId" )]
    public virtual LockerRoom LockerRoom { get; set; }
}