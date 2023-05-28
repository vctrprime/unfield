using StadiumEngine.Domain.Entities.BookingForm;

namespace StadiumEngine.Domain.Services.Facades.BookingForm;

public interface IBookingCheckoutCommandFacade
{
    Task CancelBookingAsync( string bookingNumber );
    void FillBookingData( Booking booking );
    Task ConfirmBookingAsync( string bookingNumber, string accessCode );
    
}