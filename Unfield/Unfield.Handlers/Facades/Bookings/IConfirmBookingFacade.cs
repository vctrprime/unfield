using Unfield.Commands.BookingForm;
using Unfield.DTO.BookingForm;

namespace Unfield.Handlers.Facades.Bookings;

internal interface IConfirmBookingFacade
{
    Task<ConfirmBookingDto> ConfirmAsync( ConfirmBookingCommand request );
}