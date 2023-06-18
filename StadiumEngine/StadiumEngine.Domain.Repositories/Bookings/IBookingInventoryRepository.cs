using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Domain.Repositories.Bookings;

public interface IBookingInventoryRepository
{
    void Add( IEnumerable<BookingInventory> inventories );
}