using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Services.Resolvers.Offers;

public interface ILockerRoomStatusResolver
{
    LockerRoomStatus Resolve( LockerRoom lockerRoom, DateTime clientDate );
}