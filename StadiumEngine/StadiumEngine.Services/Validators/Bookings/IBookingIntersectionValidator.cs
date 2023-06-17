using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Services.Validators.Bookings;

internal interface IBookingIntersectionValidator
{
    Task<bool> Validate( Booking booking );
}