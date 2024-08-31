using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Services.Models.Notifications;

namespace StadiumEngine.Domain.Services.Infrastructure;

public interface ISmsSender
{
    Task SendPasswordAsync( PasswordNotification passwordNotification );

    Task SendBookingAccessCodeAsync( Booking booking, string language );
    Task SendBookingConfirmation( Booking booking, string language );
}