using Unfield.Domain.Entities.Bookings;

namespace Unfield.Services.Validators.Bookings;

internal interface IBookingIntersectionValidator
{
    Task<bool> ValidateAsync( Booking booking );
}