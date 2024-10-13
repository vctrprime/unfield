using Unfield.Common.Enums.Notifications;

namespace Unfield.Domain.Services.Models.Notifications;

public class PasswordNotification
{
    public string PhoneNumber { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Language { get; set; } = null!;
    public PasswordNotificationType Type { get; set; }
    public PasswordNotificationSubject Subject { get; set; }
    public string? StadiumGroupName { get; set; }
}