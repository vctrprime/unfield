using Unfield.Domain.Entities.Notifications;

namespace Unfield.Domain.Services.Core.Notifications;

public interface IUIMessageBuilder
{
    UIMessage Build();
}