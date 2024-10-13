using Unfield.Domain.Entities.Bookings;
using Unfield.Domain.Services.Models.Notifications;

namespace Unfield.Domain.Services.Infrastructure;

public interface ISmsSender
{
    Task SendPasswordAsync( PasswordNotification passwordNotification );

    Task SendBookingAccessCodeAsync( Booking booking, string language );
    Task SendBookingConfirmation( Booking booking, string language );
}