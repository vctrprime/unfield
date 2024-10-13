using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unfield.Commands.Settings.Breaks;
using Unfield.Common.Constant;
using Unfield.DTO.Settings.Breaks;
using Unfield.Extranet.Infrastructure.Attributes;
using Unfield.Queries.Settings.Breaks;

namespace Unfield.Extranet.Controllers.API.Settings;

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