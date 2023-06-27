using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Services.Application.Offers;

namespace StadiumEngine.Services.Application.Offers;

internal class LockerRoomCommandService : ILockerRoomCommandService
{
    private readonly ILockerRoomRepository _lockerRoomRepository;

    public LockerRoomCommandService( ILockerRoomRepository lockerRoomRepository )
    {
        _lockerRoomRepository = lockerRoomRepository;
    }

    public void AddLockerRoom( LockerRoom lockerRoom ) => _lockerRoomRepository.Add( lockerRoom );

    public void UpdateLockerRoom( LockerRoom lockerRoom ) => _lockerRoomRepository.Update( lockerRoom );

    public async Task DeleteLockerRoomAsync( int lockerRoomId, int stadiumId )
    {
        LockerRoom? lockerRoom = await _lockerRoomRepository.GetAsync( lockerRoomId, stadiumId );

        if ( lockerRoom == null )
        {
            throw new DomainException( ErrorsKeys.LockerRoomNotFound );
        }

        _lockerRoomRepository.Remove( lockerRoom );
    }
}