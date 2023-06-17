using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Domain.Repositories.BookingForm;

public interface IBookingCostRepository
{
    void Add( IEnumerable<BookingCost> costs );
}