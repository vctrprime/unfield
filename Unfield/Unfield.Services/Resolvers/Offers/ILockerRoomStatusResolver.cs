using Unfield.Common.Enums.Offers;
using Unfield.Domain.Entities.Offers;

namespace Unfield.Services.Resolvers.Offers;

public interface ILockerRoomStatusResolver
{
    LockerRoomStatus Resolve( LockerRoom lockerRoom, DateTime clientDate );
}