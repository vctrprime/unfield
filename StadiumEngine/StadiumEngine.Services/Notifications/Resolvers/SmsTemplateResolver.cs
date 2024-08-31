using System.Globalization;
using System.Resources;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Entities.Notifications;
using StadiumEngine.Domain.Services.Models.Notifications;

namespace StadiumEngine.Services.Notifications.Resolvers;

internal class SmsTemplateResolver : ISmsTemplateResolver
{
    private readonly ResourceManager _notificationResourceManager;

    public SmsTemplateResolver()
    {
        _notificationResourceManager = Common.Resources.Notifications.ResourceManager;
    }

    public string ResolvePasswordNotificationTemplate( PasswordNotification passwordNotification )
    {
        string key =
            $"password_notification_{passwordNotification.Subject.ToString( "G" ).ToLower()}_{passwordNotification.Type.ToString( "G" ).ToLower()}";
        string? template = _notificationResourceManager.GetString(
            key,
            new CultureInfo( passwordNotification.Language ) );
        return template ?? String.Empty;
    }

    public string ResolveBookingAccessCodeTemplate( string language )
    {
        string? template = _notificationResourceManager.GetString( $"access_code", new CultureInfo( language ) );
        return template ?? String.Empty;
    }

    public string ResolveBookingConfirmationTemplate( Booking booking, string language )
    {
        string template =
            _notificationResourceManager.GetString( $"booking_confirmation", new CultureInfo( language ) ) ??
            String.Empty;

        if ( booking.BookingLockerRoom != null )
        {
            string lockerRoomTemplate = _notificationResourceManager.GetString(
                $"booking_confirmation_locker_room",
                new CultureInfo( language ) ) ?? String.Empty;
            lockerRoomTemplate = String.Format( lockerRoomTemplate, booking.BookingLockerRoom.LockerRoom.Name );
            template = String.Format( template, booking.Number, lockerRoomTemplate );
        }
        else
        {
            template = String.Format( template, booking.Number, String.Empty );
        }

        return template;
    }
}