using Hangfire;
using StadiumEngine.Common.Enums.Notifications;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.Domain.Services.Models.Notifications;

namespace StadiumEngine.Jobs.Background.Notifications;

internal class BackgroundNotificationSender : IBackgroundNotificationSender
{
    private readonly ISmsSender _smsSender;

    public BackgroundNotificationSender( ISmsSender smsSender )
    {
        _smsSender = smsSender;
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
}