using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Domain.Services.Facades.BookingForm;

public interface IBookingFormCommandFacade
{
    Task CreateBookingAsync( Booking booking );
}