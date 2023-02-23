using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Services.Facades.Offers;

namespace StadiumEngine.Services.Facades.Offers;

internal class LockerRoomFacade : ILockerRoomFacade
{
    private readonly ILockerRoomRepository _lockerRoomRepository;

    public LockerRoomFacade(ILockerRoomRepository lockerRoomRepository)
    {
        _lockerRoomRepository = lockerRoomRepository;
    }
    public async Task<List<LockerRoom>> GetByStadiumId(int stadiumId)
    {
        return await _lockerRoomRepository.GetAll(stadiumId);
    }

    public async Task<LockerRoom?> GetByLockerRoomId(int lockerRoomId, int stadiumId)
    {
        return await _lockerRoomRepository.Get(lockerRoomId, stadiumId);
    }

    public void AddLockerRoom(LockerRoom lockerRoom)
    {
        _lockerRoomRepository.Add(lockerRoom);
    }

    public void UpdateLockerRoom(LockerRoom lockerRoom)
    {
        _lockerRoomRepository.Update(lockerRoom);
    }

    public async Task DeleteLockerRoom(int lockerRoomId, int stadiumId)
    {
        var lockerRoom = await _lockerRoomRepository.Get(lockerRoomId, stadiumId);

        if (lockerRoom == null) throw new DomainException(ErrorsKeys.LockerRoomNotFound);
        
        _lockerRoomRepository.Remove(lockerRoom);
    }
}