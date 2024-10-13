using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unfield.Common.Constant;
using Unfield.DTO.Schedule;
using Unfield.Extranet.Infrastructure.Attributes;
using Unfield.Queries.Schedule;

namespace Unfield.Extranet.Controllers.API.Schedule;

/// <summary>
///     Расписание
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
    public async Task<SchedulerFieldsDto> GetFields( [FromRoute] GetSchedulerFieldsQuery query )
    {
        SchedulerFieldsDto fields = await Mediator.Send( query );
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
    
    /// <summary>
    ///     Получить список
    /// </summary>
    /// <returns></returns>
    [HttpGet( "list" )]
    [HasPermission( $"{PermissionsKeys.GetBookings}" )]
    public async Task<List<BookingListItemDto>> GetBookingList( [FromQuery] GetBookingListQuery query )
    {
        List<BookingListItemDto> bookings = await Mediator.Send( query );
        return bookings;
    }
}