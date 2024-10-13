using Unfield.Common;
using Unfield.Common.Enums.Offers;
using Unfield.Common.Exceptions;
using Unfield.Domain;
using Unfield.Domain.Entities.Bookings;
using Unfield.Domain.Entities.Offers;
using Unfield.Domain.Repositories.Bookings;
using Unfield.Domain.Repositories.Offers;
using Unfield.Domain.Services.Core.Offers;
using Unfield.Services.Resolvers.Offers;

namespace Unfield.Services.Core.Offers;

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