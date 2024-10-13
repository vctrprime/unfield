using Unfield.Common.Enums.Bookings;
using Unfield.DTO.BookingForm;
using Unfield.Queries.BookingForm;

namespace Unfield.Handlers.Builders.BookingForm;

internal interface IBookingCheckoutDtoBuilder
{
    Task<BookingCheckoutDto> BuildAsync( GetBookingCheckoutQuery query );
}