using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Domain.Services.Core.Schedule;

public interface ISchedulerBookingQueryService
{
    Task<Booking> GetBookingAsync( string bookingNumber );
}