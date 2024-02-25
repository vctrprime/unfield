using System;
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Entities.Bookings;

public class BookingLockerRoom : BaseUserEntity
{
    public int BookingId { get; set; }
    public int LockerRoomId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public virtual Booking Booking { get; set; }
    public virtual LockerRoom LockerRoom { get; set; }
}