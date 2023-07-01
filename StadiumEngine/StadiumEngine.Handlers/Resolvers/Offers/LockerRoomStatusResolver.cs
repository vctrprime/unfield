using AutoMapper;
using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.DTO.Offers.LockerRooms;

namespace StadiumEngine.Handlers.Resolvers.Offers;

internal class LockerRoomStatusResolver : IValueResolver<LockerRoom, LockerRoomDto, LockerRoomStatus>
{
    public LockerRoomStatus Resolve(
        LockerRoom source,
        LockerRoomDto destination,
        LockerRoomStatus destMember,
        ResolutionContext context )
    {
        DateTime now = DateTime.Now.ToUniversalTime();
        
        if (source.BookingLockerRooms.Any( x => x.Start <= now && x.End >= now))
        {
            return LockerRoomStatus.Busy;
        }

        if ( source.BookingLockerRooms.Any( x => x.End < now ) )
        {
            return LockerRoomStatus.NeedCleaning;
        }

        if ( !source.BookingLockerRooms.Any() || source.BookingLockerRooms.All( x => x.Start > now ) )
        {
            return LockerRoomStatus.Ready;
        }

        return LockerRoomStatus.Unknown;
    }
}