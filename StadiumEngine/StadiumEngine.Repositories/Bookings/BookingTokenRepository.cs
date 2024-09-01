using Microsoft.EntityFrameworkCore;
using StadiumEngine.Common.Enums.Bookings;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Repositories.Bookings;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Bookings;

internal class BookingTokenRepository : BaseRepository<BookingToken>, IBookingTokenRepository
{
    public BookingTokenRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<BookingToken?> GetAsync( string token, BookingTokenType type ) => 
        await Entities
            .Include( x => x.Booking )
            .ThenInclude( x => x.Customer )
            .Include( x => x.Booking )
            .ThenInclude( x => x.Field )
            .ThenInclude( x => x.Stadium )
            .SingleOrDefaultAsync( x => x.Token == token && x.Type == type );

    public new void Remove( BookingToken token ) => 
        base.Remove( token );
}