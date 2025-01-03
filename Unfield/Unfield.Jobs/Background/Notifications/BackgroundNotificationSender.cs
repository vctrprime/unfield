using Hangfire;
using Unfield.Common.Enums.Notifications;
using Unfield.Domain.Services.Infrastructure;
using Unfield.Domain.Services.Models.Notifications;
using Unfield.MessageContracts.Bookings;

namespace Unfield.Jobs.Background.Notifications;

internal class BackgroundNotificationSender : IBackgroundNotificationSender
{
    private readonly ISmsSender _smsSender;
    private readonly IMessagePublisher _messagePublisher;

    public BackgroundNotificationSender( ISmsSender smsSender, IMessagePublisher messagePublisher )
    {
        _smsSender = smsSender;
        _messagePublisher = messagePublisher;
    }

    [Queue( "notifications" )]
    public async Task SendPasswordAsync(
        string phoneNumber,
        string password,
        string language,
        PasswordNotificationType type,
        PasswordNotificationSubject subject,
        string? stadiumGroupName ) =>
        await _smsSender.SendPasswordAsync( new PasswordNotification
        {
            PhoneNumber = phoneNumber,
            Language = language,
            Password = password,
            Subject = subject,
            Type = type,
            StadiumGroupName = stadiumGroupName
        } );

    public async Task SendBookingConfirmedAsync( string bookingNumber ) => 
        await _messagePublisher.PublishAsync( new BookingConfirmed( bookingNumber ) );
    
    public async Task SendBookingCanceledAsync( string bookingNumber, DateTime day, string? reason ) => 
        await _messagePublisher.PublishAsync( new BookingCanceled( bookingNumber, day, reason ) );
}