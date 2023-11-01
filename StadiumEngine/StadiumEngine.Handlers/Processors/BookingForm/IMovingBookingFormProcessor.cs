using StadiumEngine.DTO.BookingForm;

namespace StadiumEngine.Handlers.Processors.BookingForm;

internal interface IMovingBookingFormProcessor
{
    Task<BookingFormDto> ProcessAsync( string bookingNumber, BookingFormDto originalBookingFormData );
}