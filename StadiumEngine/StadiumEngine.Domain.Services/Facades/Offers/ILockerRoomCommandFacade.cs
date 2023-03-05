#nullable enable
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Services.Facades.Offers;

public interface ILockerRoomCommandFacade
{
    void AddLockerRoom( LockerRoom lockerRoom );
    void UpdateLockerRoom( LockerRoom lockerRoom );
    Task DeleteLockerRoom( int lockerRoomId, int stadiumId );
}