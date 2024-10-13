using Unfield.Domain.Entities.Bookings;

namespace Unfield.Domain.Services.Core.BookingForm.Distributors;

public interface IBookingLockerRoomDistributor
{
    Task DistributeAsync( Booking booking );
}