using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Repositories.Bookings;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Bookings;

internal class BookingWeeklyExcludeDayRepository : BaseRepository<BookingWeeklyExcludeDay>, IBookingWeeklyExcludeDayRepository
{
    public BookingWeeklyExcludeDayRepository( MainDbContext context ) : base( context )
    {
    }

    public new void Add( BookingWeeklyExcludeDay excludeDay ) => base.Add( excludeDay );
}