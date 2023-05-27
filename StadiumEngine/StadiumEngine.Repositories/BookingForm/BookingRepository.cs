using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.BookingForm;
using StadiumEngine.Domain.Repositories.BookingForm;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.BookingForm;

internal class BookingRepository : BaseRepository<Booking>, IBookingRepository
{
    public BookingRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<List<Booking>> GetAsync( DateTime day, List<int> stadiumsIds ) =>
        await Entities
            .Include( x => x.Field )
            .ThenInclude( x => x.ChildFields )
            .Where(
                x => x.Day.Date == day.Date
                     && stadiumsIds.Contains( x.Field.StadiumId ) )
            .ToListAsync();

    public async Task<Booking?> GetByNumberAsync( string bookingNumber ) =>
        await Entities
            .Include( x => x.Field )
            .ThenInclude( x => x.SportKinds )
            .SingleOrDefaultAsync( x => x.Number == bookingNumber );

    public new void Add( Booking booking ) => base.Add( booking );
}