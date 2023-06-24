using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Domain.Repositories.Bookings;

public interface IBookingRepository
{
    Task<List<Booking>> GetAsync( DateTime day, List<int> stadiumsIds );
    Task<List<Booking>> GetWeeklyAsync( DateTime day, List<int> stadiumsIds );
    Task<List<Booking>> GetAsync( DateTime from, DateTime to, int stadiumId );
    Task<List<Booking>> GetWeeklyAsync( DateTime from, DateTime to, int stadiumId );
    Task<Booking?> GetByNumberAsync( string bookingNumber );
    void Add( Booking booking );
    void Update( Booking booking );
}