#nullable enable
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Services.Core.Offers;

public interface ILockerRoomQueryService
{
    Task<List<LockerRoom>> GetByStadiumIdAsync( int stadiumId );
    Task<LockerRoom?> GetByLockerRoomIdAsync( int lockerRoomId, int stadiumId );
}