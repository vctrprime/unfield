using Unfield.Commands;

namespace Unfield.Handlers.Facades.Bookings;

internal interface ICancelBookingFacade
{
    Task CancelBooking( BaseCancelBookingCommand request, int? userId, string? customerPhoneNumber );
}