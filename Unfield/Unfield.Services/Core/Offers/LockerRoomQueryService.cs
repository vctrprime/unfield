using Unfield.Domain.Entities.Offers;
using Unfield.Domain.Repositories.Offers;
using Unfield.Domain.Services.Core.Offers;

namespace Unfield.Services.Core.Offers;

internal class LockerRoomQueryService : ILockerRoomQueryService
{
    private readonly ILockerRoomRepository _lockerRoomRepository;

    public LockerRoomQueryService( ILockerRoomRepository lockerRoomRepository )
    {
        _lockerRoomRepository = lockerRoomRepository;
    }

    public async Task<List<LockerRoom>> GetByStadiumIdAsync( int stadiumId ) =>
        await _lockerRoomRepository.GetAllAsync( stadiumId );

    public async Task<LockerRoom?> GetByLockerRoomIdAsync( int lockerRoomId, int stadiumId ) =>
        await _lockerRoomRepository.GetAsync( lockerRoomId, stadiumId );
}