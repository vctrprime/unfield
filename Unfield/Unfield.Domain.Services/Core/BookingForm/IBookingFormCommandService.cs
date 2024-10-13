using Unfield.Domain.Entities.Bookings;

namespace Unfield.Domain.Services.Core.BookingForm;

public interface IBookingFormCommandService
{
    Task CreateBookingAsync( Booking booking, IUnitOfWork unitOfWork );
}