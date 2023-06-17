using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Common.Constant;
using StadiumEngine.DTO.Offers.Fields;
using StadiumEngine.Commands.Offers.Fields;
using StadiumEngine.DTO.Schedule;
using StadiumEngine.Queries.Offers.Fields;
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
    public async Task<List<SchedulerFieldDto>> GetFields()
    {
        List<FieldDto> fields = await Mediator.Send( new GetFieldsQuery() );
        return fields.Where( x => x.IsActive ).Select( x => new SchedulerFieldDto( x ) ).ToList();
    }
}