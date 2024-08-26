using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Domain.Repositories.Bookings;

public interface IBookingRepository
{
    Task<List<Booking>> GetAsync( DateTime day, List<int> stadiumsIds );
    Task<List<Booking>> GetWeeklyAsync( DateTime day, List<int> stadiumsIds );
    Task<List<Booking>> GetAsync( DateTime from, DateTime to, int stadiumId );
    Task<List<Booking>> GetWeeklyAsync( int stadiumId );
    Task<Booking?> GetByNumberAsync( string bookingNumber );
    Task<Booking?> FindDraftAsync( DateTime day, decimal startHour, int fieldId );
    void Add( Booking booking );
    void Update( Booking booking );
    Task<int> DeleteDraftsByDateAsync( DateTime date, int limit );
    Booking DetachedClone( Booking booking );
    Task<List<Booking>> SearchAllByNumberAsync( string bookingNumber, int stadiumId );
}