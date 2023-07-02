using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Domain.Repositories.Bookings;

public interface IBookingLockerRoomRepository
{
    Task<List<BookingLockerRoom>> Get( DateTime from, DateTime to, int stadiumId );
    void Update( BookingLockerRoom bookingLockerRoom );
    void Remove( IEnumerable<BookingLockerRoom> bookingLockerRooms );
}