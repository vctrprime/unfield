using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unfield.Commands.Notifications;
using Unfield.DTO.Notifications;
using Unfield.Queries.Notifications;

namespace Unfield.Extranet.Controllers.API.Notifications;

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
    
    /// <summary>
    ///     Отметить последнее прочитанное сообщение
    /// </summary>
    /// <returns></returns>
    [HttpPost("read")]
    public async Task<SetLastReadUIMessageDto> SetLastReadMessage( [FromBody] SetLastReadUIMessageCommand command )
    {
        SetLastReadUIMessageDto dto = await Mediator.Send( command );

        return dto;
    }
}