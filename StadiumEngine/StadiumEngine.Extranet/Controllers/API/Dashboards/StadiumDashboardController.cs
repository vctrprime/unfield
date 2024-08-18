using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Common.Constant;
using StadiumEngine.DTO.Dashboards;
using StadiumEngine.Extranet.Infrastructure.Attributes;
using StadiumEngine.Queries.Dashboards;

namespace StadiumEngine.Extranet.Controllers.API.Dashboards;

/// <summary>
///     Дашборд стадиона
/// </summary>
[Route( "api/dashboard/stadium" )]
public class StadiumDashboardController : BaseApiController
{
    /// <summary>
    ///     Получить дашборд
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [HasPermission( $"{PermissionsKeys.ViewDashboard}" )]
    public async Task<StadiumDashboardDto> Get( [FromQuery] GetStadiumDashboardQuery query )
    {
        StadiumDashboardDto dashboard = await Mediator.Send( query );
        return dashboard;
    }
}