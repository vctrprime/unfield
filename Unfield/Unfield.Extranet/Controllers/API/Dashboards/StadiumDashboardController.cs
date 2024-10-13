using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unfield.Common.Constant;
using Unfield.DTO.Dashboards;
using Unfield.Extranet.Infrastructure.Attributes;
using Unfield.Queries.Dashboards;

namespace Unfield.Extranet.Controllers.API.Dashboards;

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