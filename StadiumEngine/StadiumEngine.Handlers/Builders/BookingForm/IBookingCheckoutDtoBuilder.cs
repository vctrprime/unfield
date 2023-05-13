using StadiumEngine.DTO.BookingForm;
using StadiumEngine.Queries.BookingForm;

namespace StadiumEngine.Handlers.Builders.BookingForm;

internal interface IBookingCheckoutDtoBuilder
{
    Task<BookingCheckoutDto> BuildAsync( GetBookingCheckoutQuery query );
}