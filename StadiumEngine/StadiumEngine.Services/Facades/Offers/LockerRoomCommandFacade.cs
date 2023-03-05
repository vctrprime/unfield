using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Services.Facades.Offers;

namespace StadiumEngine.Services.Facades.Offers;

internal class LockerRoomCommandFacade : ILockerRoomCommandFacade
{
    private readonly ILockerRoomRepository _lockerRoomRepository;

    public LockerRoomCommandFacade( ILockerRoomRepository lockerRoomRepository )
    {
        _lockerRoomRepository = lockerRoomRepository;
    }

    public void AddLockerRoom( LockerRoom lockerRoom )
    {
        _lockerRoomRepository.Add( lockerRoom );
    }

    public void UpdateLockerRoom( LockerRoom lockerRoom )
    {
        _lockerRoomRepository.Update( lockerRoom );
    }

    public async Task DeleteLockerRoom( int lockerRoomId, int stadiumId )
    {
        var lockerRoom = await _lockerRoomRepository.Get( lockerRoomId, stadiumId );

        if (lockerRoom == null) throw new DomainException( ErrorsKeys.LockerRoomNotFound );

        _lockerRoomRepository.Remove( lockerRoom );
    }
}