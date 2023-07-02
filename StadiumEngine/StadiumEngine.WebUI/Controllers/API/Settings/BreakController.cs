using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Commands.Settings.Breaks;
using StadiumEngine.Common.Constant;
using StadiumEngine.DTO.Settings.Breaks;
using StadiumEngine.Queries.Settings.Breaks;
using StadiumEngine.WebUI.Infrastructure.Attributes;

namespace StadiumEngine.WebUI.Controllers.API.Settings;

/// <summary>
///     Перерывы
/// </summary>
[Route( "api/settings/breaks" )]
public class BreakController : BaseApiController
{
    /// <summary>
    ///     Получить перерывы
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [HasPermission( PermissionsKeys.GetBreaks )]
    public async Task<List<BreakDto>> GetAll( [FromQuery] GetBreaksQuery query )
    {
        List<BreakDto> breaks = await Mediator.Send( query );
        return breaks;
    }
    
    /// <summary>
    ///     Получить перерыв
    /// </summary>
    /// <returns></returns>
    [HttpGet( "{breakId}" )]
    [HasPermission( PermissionsKeys.GetBreaks )]
    public async Task<BreakDto> Get( [FromRoute] GetBreakQuery query )
    {
        BreakDto @break = await Mediator.Send( query );
        return @break;
    }

    /// <summary>
    ///     Добавить перерыв
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [HasPermission( PermissionsKeys.InsertBreak )]
    public async Task<AddBreakDto> Post( [FromBody] AddBreakCommand command )
    {
        AddBreakDto dto = await Mediator.Send( command );
        return dto;
    }

    /// <summary>
    ///     Обновить перерыв
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [HasPermission( PermissionsKeys.UpdateBreak )]
    public async Task<UpdateBreakDto> Put( [FromBody] UpdateBreakCommand command )
    {
        UpdateBreakDto dto = await Mediator.Send( command );
        return dto;
    }

    /// <summary>
    ///     Удалить перерыв
    /// </summary>
    /// <returns></returns>
    [HttpDelete( "{breakId}" )]
    [HasPermission( PermissionsKeys.DeleteBreak )]
    public async Task<DeleteBreakDto> Delete( [FromRoute] DeleteBreakCommand command )
    {
        DeleteBreakDto dto = await Mediator.Send( command );
        return dto;
    }
}