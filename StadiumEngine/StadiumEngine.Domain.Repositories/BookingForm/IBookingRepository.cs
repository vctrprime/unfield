using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Domain.Repositories.BookingForm;

public interface IBookingRepository
{
    Task<List<Booking>> GetAsync( DateTime day, List<int> stadiumsIds );
    Task<Booking?> GetByNumberAsync( string bookingNumber );
    void Add( Booking booking );
    void Update( Booking booking );
}