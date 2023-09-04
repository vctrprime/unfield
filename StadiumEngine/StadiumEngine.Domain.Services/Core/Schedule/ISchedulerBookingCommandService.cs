using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Domain.Services.Core.Schedule;

public interface ISchedulerBookingCommandService
{
    Task FillBookingDataAsync( Booking booking, bool autoLockerRoom );
    Task AddVersionAsync( Booking booking );
    void UpdateOldVersion( Booking booking );
    void AddExcludeDay( int bookingId, DateTime day, int userId, string? reason = null );
}