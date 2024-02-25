#nullable enable
using System;

namespace StadiumEngine.Domain.Entities.Bookings;

public class BookingWeeklyExcludeDay : BaseUserEntity
{
    public int BookingId { get; set; }
    public DateTime Day { get; set; }
    public string? Reason { get; set; }
    
    public virtual Booking Booking { get; set; } = null!;
}