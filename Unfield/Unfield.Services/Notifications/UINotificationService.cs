using Unfield.Domain.Entities.Notifications;
using Unfield.Domain.Services.Core.Notifications;
using Unfield.Domain.Services.Utils;

namespace Unfield.Services.Notifications;

public class UINotificationService : IUINotificationService
{
    private readonly IUIMessageCommandService _commandService;
    private readonly IUtilService _utilService;

    public UINotificationService( IUIMessageCommandService commandService, IUtilService utilService )
    {
        _commandService = commandService;
        _utilService = utilService;
    }
    
    
    public async Task Notify( IUIMessageBuilder builder )
    {
        UIMessage message = builder.Build();
        _commandService.Add( message );

        await _utilService.NewUIMessage( message.StadiumId );
    }
}