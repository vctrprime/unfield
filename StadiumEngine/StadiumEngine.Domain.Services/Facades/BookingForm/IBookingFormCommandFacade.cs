using StadiumEngine.Domain.Entities.BookingForm;

namespace StadiumEngine.Domain.Services.Facades.BookingForm;

public interface IBookingFormCommandFacade
{
    Task CreateBookingAsync( Booking booking );
}