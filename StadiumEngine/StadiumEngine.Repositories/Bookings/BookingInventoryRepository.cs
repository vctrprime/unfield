using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Repositories.Bookings;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Bookings;

internal class BookingInventoryRepository : BaseRepository<BookingInventory>, IBookingInventoryRepository
{
    public BookingInventoryRepository( MainDbContext context ) : base( context )
    {
    }

    public new void Add( IEnumerable<BookingInventory> inventories ) => base.Add( inventories );
}