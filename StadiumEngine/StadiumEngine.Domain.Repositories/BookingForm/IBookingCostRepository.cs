using StadiumEngine.Domain.Entities.BookingForm;

namespace StadiumEngine.Domain.Repositories.BookingForm;

public interface IBookingCostRepository
{
    void Add( IEnumerable<BookingCost> costs );
}