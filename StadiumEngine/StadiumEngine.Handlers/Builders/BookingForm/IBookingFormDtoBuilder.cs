using StadiumEngine.DTO.BookingForm;
using StadiumEngine.Queries.BookingForm;

namespace StadiumEngine.Handlers.Builders.BookingForm;

internal interface IBookingFormDtoBuilder
{
    Task<BookingFormDto> BuildAsync( GetBookingFormQuery query );
    Task<BookingFormDto> BuildAsync( int fieldId, DateTime day, int currentHour );
}