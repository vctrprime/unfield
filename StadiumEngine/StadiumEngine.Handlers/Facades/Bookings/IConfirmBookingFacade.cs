using StadiumEngine.Commands.BookingForm;
using StadiumEngine.DTO.BookingForm;

namespace StadiumEngine.Handlers.Facades.Bookings;

internal interface IConfirmBookingFacade
{
    Task<ConfirmBookingDto> ConfirmAsync( ConfirmBookingCommand request, CancellationToken cancellationToken );
}