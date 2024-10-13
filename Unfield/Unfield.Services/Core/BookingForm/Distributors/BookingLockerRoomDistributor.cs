using Unfield.Domain.Entities.Bookings;
using Unfield.Domain.Entities.Offers;
using Unfield.Domain.Repositories.Bookings;
using Unfield.Domain.Repositories.Offers;
using Unfield.Domain.Services.Core.BookingForm.Distributors;

namespace Unfield.Services.Core.BookingForm.Distributors;

internal class BookingLockerRoomDistributor : IBookingLockerRoomDistributor
{
    private readonly ILockerRoomRepository _lockerRoomRepository;
    private readonly IBookingLockerRoomRepository _bookingLockerRoomRepository;

    public BookingLockerRoomDistributor( ILockerRoomRepository lockerRoomRepository, IBookingLockerRoomRepository bookingLockerRoomRepository )
    {
        _lockerRoomRepository = lockerRoomRepository;
        _bookingLockerRoomRepository = bookingLockerRoomRepository;
    }

    public async Task DistributeAsync( Booking booking )
    {
        //раздевалка занята за полчаса до начала и еще полчаса после
        DateTime bookingLockerRoomStart = booking.Day.AddHours(( double )booking.StartHour - 0.5);
        DateTime bookingLockerRoomEnd = booking.Day.AddHours(( double )( booking.StartHour + booking.HoursCount ) + 0.5);
        
        List<LockerRoom> lockerRooms = await _lockerRoomRepository.GetAllAsync( booking.Field.StadiumId );
        List<BookingLockerRoom> bookingLockerRooms = await _bookingLockerRoomRepository.Get(
            bookingLockerRoomStart,
            bookingLockerRoomEnd,
            booking.Field.StadiumId );

        List<LockerRoom> availableLockerRooms =
            lockerRooms.Where( x => x.IsActive && !bookingLockerRooms.Select( blr => blr.LockerRoomId ).Contains( x.Id ) ).ToList();

        if ( availableLockerRooms.Any() )
        {
            LockerRoom lockerRoom = availableLockerRooms.OrderBy( x => x.BookingLockerRooms.Count ).First();
            booking.BookingLockerRoom = new BookingLockerRoom
            {
                LockerRoom = lockerRoom,
                Start = bookingLockerRoomStart,
                End = bookingLockerRoomEnd
            };
        }
    }
}