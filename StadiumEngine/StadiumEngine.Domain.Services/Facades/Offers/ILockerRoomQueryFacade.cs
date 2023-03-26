#nullable enable
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Services.Facades.Offers;

public interface ILockerRoomQueryFacade
{
    Task<List<LockerRoom>> GetByStadiumIdAsync( int stadiumId );
    Task<LockerRoom?> GetByLockerRoomIdAsync( int lockerRoomId, int stadiumId );
}