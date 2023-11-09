using Microsoft.AspNetCore.SignalR;
using StadiumEngine.Common.Hubs;
using StadiumEngine.Domain.Entities.Notifications;
using StadiumEngine.Domain.Services.Core.Notifications;

namespace StadiumEngine.Services.Notifications;

public class UINotificationService : IUINotificationService
{
    private readonly IUIMessageCommandService _commandService;
    private readonly IHubContext<StadiumHub> _stadiumHubContext;

    public UINotificationService( IUIMessageCommandService commandService, IHubContext<StadiumHub> stadiumHubContext )
    {
        _commandService = commandService;
        _stadiumHubContext = stadiumHubContext;
    }
    
    
    public async Task Notify( IUIMessageBuilder builder )
    {
        UIMessage message = builder.Build();
        _commandService.Add( message );

        await _stadiumHubContext.Clients.Group( $"stadium-{message.StadiumId}" ).SendAsync( "HasNewUIMessages" );
    }
}