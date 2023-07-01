using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Repositories.Bookings;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Bookings;

internal class BookingLockerRoomRepository : BaseRepository<BookingLockerRoom>, IBookingLockerRoomRepository
{
    public BookingLockerRoomRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<List<BookingLockerRoom>> Get( DateTime from, DateTime to, int stadiumId ) =>
        await Entities
            .Include( x => x.LockerRoom )
            .Where(
            x => (
                ( x.Start > from && x.Start < to )
                ||
                ( x.End > from && x.End < to ) ) && x.LockerRoom.StadiumId == stadiumId ).ToListAsync();

    public new void Add( BookingLockerRoom bookingLockerRoom ) => base.Add( bookingLockerRoom );

    public new void Update( BookingLockerRoom bookingLockerRoom ) => base.Update( bookingLockerRoom );

    public new void Remove( IEnumerable<BookingLockerRoom> bookingLockerRooms ) => base.Remove( bookingLockerRooms );
}