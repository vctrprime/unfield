using StadiumEngine.Common;
using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.Bookings;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Services.Core.Offers;
using StadiumEngine.Services.Resolvers.Offers;

namespace StadiumEngine.Services.Core.Offers;

internal class LockerRoomCommandService : ILockerRoomCommandService
{
    private readonly ILockerRoomRepository _lockerRoomRepository;
    private readonly IBookingLockerRoomRepository _bookingLockerRoomRepository;
    private readonly ILockerRoomStatusResolver _lockerRoomStatusResolver;
    private readonly IUnitOfWork _unitOfWork;

    public LockerRoomCommandService(
        ILockerRoomRepository lockerRoomRepository,
        IBookingLockerRoomRepository bookingLockerRoomRepository,
        ILockerRoomStatusResolver lockerRoomStatusResolver,
        IUnitOfWork unitOfWork )
    {
        _lockerRoomRepository = lockerRoomRepository;
        _bookingLockerRoomRepository = bookingLockerRoomRepository;
        _lockerRoomStatusResolver = lockerRoomStatusResolver;
        _unitOfWork = unitOfWork;
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

    public async Task<LockerRoomStatus> SyncStatusAsync( int lockerRoomId, int stadiumId, DateTime clientDate )
    {
        LockerRoom? lockerRoom = await _lockerRoomRepository.GetAsync( lockerRoomId, stadiumId );

        if ( lockerRoom == null )
        {
            throw new DomainException( ErrorsKeys.LockerRoomNotFound );
        }

        List<BookingLockerRoom> obsolete = lockerRoom.BookingLockerRooms.Where( x => x.End < clientDate ).ToList();
        _bookingLockerRoomRepository.Remove( obsolete );
        
        await _unitOfWork.SaveChangesAsync();
        
        return _lockerRoomStatusResolver.Resolve( lockerRoom, clientDate );
    }
}