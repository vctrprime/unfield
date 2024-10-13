using Unfield.Common.Enums.Bookings;
using Unfield.Domain.Entities.Bookings;

namespace Unfield.Domain.Repositories.Bookings;

public interface IBookingTokenRepository
{
    Task<BookingToken?> GetAsync( string token, BookingTokenType type );
    void Remove( BookingToken token );
}