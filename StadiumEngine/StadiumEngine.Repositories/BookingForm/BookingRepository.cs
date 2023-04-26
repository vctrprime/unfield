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
            .Where( x => x.Day == day.ToUniversalTime() && stadiumsIds.Contains( x.Field.StadiumId ) && !x.IsCanceled )
            .ToListAsync();
    
    public new void Add( Booking booking ) => base.Add( booking );
}