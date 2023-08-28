using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Domain.Services.Core.BookingForm;

public interface IBookingFormCommandService
{
    Task CreateBookingAsync( Booking booking, IUnitOfWork unitOfWork );
}