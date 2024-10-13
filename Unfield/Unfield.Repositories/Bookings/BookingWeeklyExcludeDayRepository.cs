using Unfield.Domain.Entities.Bookings;
using Unfield.Domain.Repositories.Bookings;
using Unfield.Repositories.Infrastructure.Contexts;

namespace Unfield.Repositories.Bookings;

internal class BookingWeeklyExcludeDayRepository : BaseRepository<BookingWeeklyExcludeDay>, IBookingWeeklyExcludeDayRepository
{
    public BookingWeeklyExcludeDayRepository( MainDbContext context ) : base( context )
    {
    }

    public new void Add( BookingWeeklyExcludeDay excludeDay ) => base.Add( excludeDay );
}