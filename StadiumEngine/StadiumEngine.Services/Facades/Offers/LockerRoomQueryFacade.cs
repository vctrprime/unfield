using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
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

    public async Task<List<LockerRoom>> GetByStadiumId( int stadiumId )
    {
        return await _lockerRoomRepository.GetAll( stadiumId );
    }

    public async Task<LockerRoom?> GetByLockerRoomId( int lockerRoomId, int stadiumId )
    {
        return await _lockerRoomRepository.Get( lockerRoomId, stadiumId );
    }
}