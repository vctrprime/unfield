using StadiumEngine.Common.Enums.Bookings;
using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Domain.Repositories.Bookings;

public interface IBookingTokenRepository
{
    Task<BookingToken?> GetAsync( string token, BookingTokenType type );
    void Remove( BookingToken token );
}