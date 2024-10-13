using Microsoft.EntityFrameworkCore;
using Unfield.Common.Enums.Bookings;
using Unfield.Domain.Entities.Bookings;
using Unfield.Domain.Repositories.Bookings;
using Unfield.Repositories.Infrastructure.Contexts;

namespace Unfield.Repositories.Bookings;

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
            .ThenInclude( x => x.StadiumGroup )
            .SingleOrDefaultAsync( x => x.Token == token && x.Type == type );

    public new void Remove( BookingToken token ) => 
        base.Remove( token );
}