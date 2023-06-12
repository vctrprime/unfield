using StadiumEngine.Domain.Entities.BookingForm;

namespace StadiumEngine.Services.Validators.Bookings;

internal interface IBookingIntersectionValidator
{
    Task<bool> Validate( Booking booking );
}