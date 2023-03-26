using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Services.Facades.Offers;

namespace StadiumEngine.Services.Facades.Offers;

internal class LockerRoomQueryFacade : ILockerRoomQueryFacade
{
    private readonly ILockerRoomRepository _lockerRoomRepository;

    public LockerRoomQueryFacade( ILockerRoomRepository lockerRoomRepository )
    {
        _lockerRoomRepository = lockerRoomRepository;
    }

    public async Task<List<LockerRoom>> GetByStadiumIdAsync( int stadiumId ) =>
        await _lockerRoomRepository.GetAllAsync( stadiumId );

    public async Task<LockerRoom?> GetByLockerRoomIdAsync( int lockerRoomId, int stadiumId ) =>
        await _lockerRoomRepository.GetAsync( lockerRoomId, stadiumId );
}