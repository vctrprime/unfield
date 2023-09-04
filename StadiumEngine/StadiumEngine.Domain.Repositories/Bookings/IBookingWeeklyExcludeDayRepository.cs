using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Domain.Repositories.Bookings;

public interface IBookingWeeklyExcludeDayRepository
{
    void Add( BookingWeeklyExcludeDay excludeDay );
}