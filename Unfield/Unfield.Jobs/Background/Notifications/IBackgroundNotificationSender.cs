using Unfield.Common.Enums.Notifications;

namespace Unfield.Jobs.Background.Notifications;

internal interface IBackgroundNotificationSender
{
    Task SendPasswordAsync(
        string phoneNumber,
        string password,
        string language,
        PasswordNotificationType type,
        PasswordNotificationSubject subject,
        string? stadiumGroupName );

    Task SendBookingConfirmedAsync( string bookingNumber );
    Task SendBookingCanceledAsync( string bookingNumber, DateTime day, string? reason );

}