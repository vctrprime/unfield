#nullable enable
using System.Collections.Generic;
using System.Threading.Tasks;
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Services.Facades.Offers;

public interface ILockerRoomFacade
{
    Task<List<LockerRoom>> GetByStadiumId(int stadiumId);
    Task<LockerRoom?> GetByLockerRoomId(int lockerRoomId, int stadiumId);
    void AddLockerRoom(LockerRoom lockerRoom);
    void UpdateLockerRoom(LockerRoom lockerRoom);
    Task DeleteLockerRoom(int lockerRoomId, int stadiumId);
}