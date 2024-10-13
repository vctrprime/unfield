using Microsoft.EntityFrameworkCore;
using Unfield.Domain.Entities.Bookings;
using Unfield.Domain.Repositories.Bookings;
using Unfield.Repositories.Infrastructure.Contexts;

namespace Unfield.Repositories.Bookings;

internal class BookingLockerRoomRepository : BaseRepository<BookingLockerRoom>, IBookingLockerRoomRepository
{
    public BookingLockerRoomRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<List<BookingLockerRoom>> Get( DateTime from, DateTime to, int stadiumId ) =>
        await Entities
            .Include( x => x.LockerRoom )
            .Where(
                x => (x.Start == from
                      || x.End == to
                      || ( x.Start > from && x.Start < to )
                      || ( x.End > from && x.End < to ) ) && x.LockerRoom.StadiumId == stadiumId ).ToListAsync();
    
    public new void Remove( IEnumerable<BookingLockerRoom> bookingLockerRooms ) => base.Remove( bookingLockerRooms );
}