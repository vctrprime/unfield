using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Domain.Repositories.BookingForm;

public interface IBookingInventoryRepository
{
    void Add( IEnumerable<BookingInventory> inventories );
}