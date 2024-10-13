using Unfield.Domain.Entities.Bookings;

namespace Unfield.Domain.Repositories.Bookings;

public interface IBookingLockerRoomRepository
{
    Task<List<BookingLockerRoom>> Get( DateTime from, DateTime to, int stadiumId );
    void Remove( IEnumerable<BookingLockerRoom> bookingLockerRooms );
}