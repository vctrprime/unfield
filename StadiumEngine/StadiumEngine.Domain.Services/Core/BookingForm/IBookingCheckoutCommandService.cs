using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Domain.Services.Core.BookingForm;

public interface IBookingCheckoutCommandService
{
    Task CancelBookingAsync( string bookingNumber );
    Task FillBookingDataAsync( Booking booking );
    Task<Booking> ConfirmBookingAsync( string bookingNumber, string accessCode );
    
}