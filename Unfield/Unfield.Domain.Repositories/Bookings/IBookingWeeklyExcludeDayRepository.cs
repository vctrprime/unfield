using Unfield.Domain.Entities.Bookings;

namespace Unfield.Domain.Repositories.Bookings;

public interface IBookingWeeklyExcludeDayRepository
{
    void Add( BookingWeeklyExcludeDay excludeDay );
}