using Unfield.DTO.BookingForm;

namespace Unfield.Handlers.Processors.BookingForm;

internal interface IMovingBookingFormProcessor
{
    Task<BookingFormDto> ProcessAsync( string bookingNumber, BookingFormDto originalBookingFormData );
}