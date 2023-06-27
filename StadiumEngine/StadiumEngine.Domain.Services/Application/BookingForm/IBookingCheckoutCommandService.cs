using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Domain.Services.Application.BookingForm;

public interface IBookingCheckoutCommandService
{
    Task CancelBookingAsync( string bookingNumber );
    Task FillBookingDataAsync( Booking booking );
    Task ConfirmBookingAsync( string bookingNumber, string accessCode );
    
}