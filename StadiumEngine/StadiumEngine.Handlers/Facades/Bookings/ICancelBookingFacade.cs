using StadiumEngine.Commands;

namespace StadiumEngine.Handlers.Facades.Bookings;

internal interface ICancelBookingFacade
{
    Task CancelBooking( BaseCancelBookingCommand request, int? userId, string? customerPhoneNumber );
}