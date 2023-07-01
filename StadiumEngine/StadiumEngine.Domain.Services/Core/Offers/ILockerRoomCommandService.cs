#nullable enable
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Services.Core.Offers;

public interface ILockerRoomCommandService
{
    void AddLockerRoom( LockerRoom lockerRoom );
    void UpdateLockerRoom( LockerRoom lockerRoom );
    Task DeleteLockerRoomAsync( int lockerRoomId, int stadiumId );
}