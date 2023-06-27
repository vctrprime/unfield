using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Domain.Services.Application.BookingForm;

public interface IBookingFormCommandService
{
    Task CreateBookingAsync( Booking booking );
}