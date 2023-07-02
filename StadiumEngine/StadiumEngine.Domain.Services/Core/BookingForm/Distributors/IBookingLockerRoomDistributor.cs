using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Domain.Services.Core.BookingForm.Distributors;

public interface IBookingLockerRoomDistributor
{
    Task DistributeAsync( Booking booking );
}