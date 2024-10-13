using Unfield.DTO.BookingForm;
using Unfield.Queries.BookingForm;

namespace Unfield.Handlers.Builders.BookingForm;

internal interface IBookingFormDtoBuilder
{
    Task<BookingFormDto> BuildAsync( GetBookingFormQuery query );
    Task<BookingFormDto> BuildAsync( GetBookingFormForMoveQuery query );
    Task<BookingFormDto> BuildAsync(
        int fieldId,
        DateTime day,
        int currentHour,
        string currentBookingNumber );
}