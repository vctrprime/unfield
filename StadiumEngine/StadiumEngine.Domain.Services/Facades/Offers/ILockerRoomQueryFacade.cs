#nullable enable
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Services.Facades.Offers;

public interface ILockerRoomQueryFacade
{
    Task<List<LockerRoom>> GetByStadiumId(int stadiumId);
    Task<LockerRoom?> GetByLockerRoomId(int lockerRoomId, int stadiumId);
}