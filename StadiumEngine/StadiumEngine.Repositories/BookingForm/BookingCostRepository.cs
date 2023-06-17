using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Repositories.BookingForm;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.BookingForm;

internal class BookingCostRepository : BaseRepository<BookingCost>, IBookingCostRepository
{
    public BookingCostRepository( MainDbContext context ) : base( context )
    {
    }

    public new void Add( IEnumerable<BookingCost> costs ) => base.Add( costs );
}