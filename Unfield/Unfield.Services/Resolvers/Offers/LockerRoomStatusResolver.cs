using Unfield.Common.Enums.Offers;
using Unfield.Domain.Entities.Offers;

namespace Unfield.Services.Resolvers.Offers;

internal class LockerRoomStatusResolver : ILockerRoomStatusResolver
{
    public LockerRoomStatus Resolve( LockerRoom lockerRoom, DateTime clientDate )
    {
        if (lockerRoom.BookingLockerRooms.Any( x => x.Start <= clientDate && x.End >= clientDate))
        {
            return LockerRoomStatus.Busy;
        }

        if ( lockerRoom.BookingLockerRooms.Any( x => x.End < clientDate ) )
        {
            return LockerRoomStatus.NeedCleaning;
        }

        if ( !lockerRoom.BookingLockerRooms.Any() || lockerRoom.BookingLockerRooms.All( x => x.Start > clientDate ) )
        {
            return LockerRoomStatus.Ready;
        }

        return LockerRoomStatus.Unknown;
    }
}