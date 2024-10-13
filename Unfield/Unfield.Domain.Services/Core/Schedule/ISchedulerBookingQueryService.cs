using Unfield.Domain.Entities.Bookings;

namespace Unfield.Domain.Services.Core.Schedule;

public interface ISchedulerBookingQueryService
{
    Task<Booking> GetBookingAsync( string bookingNumber, bool includeCancelled = false );
}