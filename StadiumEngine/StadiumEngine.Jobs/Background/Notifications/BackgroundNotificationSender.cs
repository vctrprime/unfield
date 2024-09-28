using Hangfire;
using StadiumEngine.Common.Enums.Notifications;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.Domain.Services.Models.Notifications;
using StadiumEngine.MessageContracts.Bookings;

namespace StadiumEngine.Jobs.Background.Notifications;

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