using StadiumEngine.Domain.Entities.BookingForm;

namespace StadiumEngine.Domain.Repositories.BookingForm;

public interface IBookingInventoryRepository
{
    void Add( IEnumerable<BookingInventory> inventories );
}