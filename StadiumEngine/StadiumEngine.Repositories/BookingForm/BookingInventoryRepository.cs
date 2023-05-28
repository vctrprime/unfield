using StadiumEngine.Domain.Entities.BookingForm;
using StadiumEngine.Domain.Repositories.BookingForm;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.BookingForm;

internal class BookingInventoryRepository : BaseRepository<BookingInventory>, IBookingInventoryRepository
{
    public BookingInventoryRepository( MainDbContext context ) : base( context )
    {
    }

    public new void Add( IEnumerable<BookingInventory> inventories ) => base.Add( inventories );
}