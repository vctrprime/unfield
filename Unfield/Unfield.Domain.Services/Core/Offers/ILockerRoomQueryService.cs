#nullable enable
using Unfield.Domain.Entities.Offers;

namespace Unfield.Domain.Services.Core.Offers;

public interface ILockerRoomQueryService
{
    Task<List<LockerRoom>> GetByStadiumIdAsync( int stadiumId );
    Task<LockerRoom?> GetByLockerRoomIdAsync( int lockerRoomId, int stadiumId );
}