using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Domain.Repositories.Bookings;

public interface IBookingCostRepository
{
    void Add( IEnumerable<BookingCost> costs );
}