using Microsoft.EntityFrameworkCore;
using Unfield.Domain.Entities.Offers;
using Unfield.Domain.Repositories.Offers;
using Unfield.Repositories.Infrastructure.Contexts;

namespace Unfield.Repositories.Offers;

internal class LockerRoomRepository : BaseRepository<LockerRoom>, ILockerRoomRepository
{
    public LockerRoomRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<List<LockerRoom>> GetAllAsync( int stadiumId ) =>
        await Entities
            .Include( lr => lr.BookingLockerRooms.Where( blr => blr.Booking.IsLastVersion ) )
            .Where( lr => lr.StadiumId == stadiumId && !lr.IsDeleted ).ToListAsync();

    public async Task<LockerRoom?> GetAsync( int lockerRoomId, int stadiumId ) =>
        await Entities
            .Include( lr => lr.BookingLockerRooms.Where( blr => blr.Booking.IsLastVersion ) )
            .FirstOrDefaultAsync(
            lr => lr.Id == lockerRoomId && lr.StadiumId == stadiumId && !lr.IsDeleted );

    public new void Add( LockerRoom lockerRoom ) => base.Add( lockerRoom );

    public new void Update( LockerRoom lockerRoom ) => base.Update( lockerRoom );

    public new void Remove( LockerRoom lockerRoom )
    {
        lockerRoom.IsDeleted = true;
        base.Update( lockerRoom );
    }
}