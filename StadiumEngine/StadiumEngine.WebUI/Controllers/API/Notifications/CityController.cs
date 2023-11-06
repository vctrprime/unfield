using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Notifications;
using StadiumEngine.Queries.Notifications;

namespace StadiumEngine.WebUI.Controllers.API.Notifications;

/// <summary>
///     UI-оповещения
/// </summary>
[Route( "api/notifications/ui-messages" )]
[AllowAnonymous]
public class UIMessageController : BaseApiController
{
    /// <summary>
    ///     Получить оповещения
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<UIMessageDto>> Get( [FromQuery] GetUIMessagesQuery query )
    {
        List<UIMessageDto> messages = await Mediator.Send( query );

        return messages;
    }
}