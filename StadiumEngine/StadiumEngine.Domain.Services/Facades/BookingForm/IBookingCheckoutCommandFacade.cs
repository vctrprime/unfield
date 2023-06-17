using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Domain.Services.Facades.BookingForm;

public interface IBookingCheckoutCommandFacade
{
    Task CancelBookingAsync( string bookingNumber );
    Task FillBookingDataAsync( Booking booking );
    Task ConfirmBookingAsync( string bookingNumber, string accessCode );
    
}