using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StadiumEngine.Common.Hubs;
using StadiumEngine.Extranet.Infrastructure.Attributes;

namespace StadiumEngine.Extranet.Controllers.Utils;

/// <summary>
/// Контроллер для оповещений
/// </summary>
[Route( "utils/notifications" )]
public class NotificationController : ControllerBase
{
    private readonly IHubContext<StadiumHub> _stadiumHubContext;

    /// <summary>
    /// Контроллер для оповещений
    /// </summary>
    public NotificationController( IHubContext<StadiumHub> stadiumHubContext )
    {
        _stadiumHubContext = stadiumHubContext;
    }
    
    /// <summary>
    /// Новое оповещений для стадиона
    /// </summary>
    /// <param name="stadiumId"></param>
    [HttpPost("new-ui-message/{stadiumId}")]
    [SecuredUtil]
    public async Task NewUIMessage( int stadiumId ) => 
        await _stadiumHubContext.Clients.Group( $"stadium-{stadiumId}" ).SendAsync( "HasNewUIMessages" );
}