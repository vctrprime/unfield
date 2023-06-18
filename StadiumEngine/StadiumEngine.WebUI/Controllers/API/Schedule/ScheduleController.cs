using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Common.Constant;
using StadiumEngine.DTO.Schedule;
using StadiumEngine.Queries.Schedule;
using StadiumEngine.WebUI.Infrastructure.Attributes;

namespace StadiumEngine.WebUI.Controllers.API.Schedule;

/// <summary>
///     Площадки
/// </summary>
[Route( "api/schedule" )]
public class ScheduleController : BaseApiController
{
    /// <summary>
    ///     Получить площадки
    /// </summary>
    /// <returns></returns>
    [HttpGet( "fields" )]
    [HasPermission( $"{PermissionsKeys.GetBookings}" )]
    public async Task<SchedulerFieldsDto> GetFields()
    {
        SchedulerFieldsDto fields = await Mediator.Send( new GetSchedulerFieldsQuery() );
        return fields;
    }

    /// <summary>
    ///     Получить события
    /// </summary>
    /// <returns></returns>
    [HttpGet( "events" )]
    [HasPermission( $"{PermissionsKeys.GetBookings}" )]
    public async Task<List<SchedulerEventDto>> GetEvents( [FromQuery] GetSchedulerEventsQuery query )
    {
        List<SchedulerEventDto> events = await Mediator.Send( query );
        return events;
    }
}