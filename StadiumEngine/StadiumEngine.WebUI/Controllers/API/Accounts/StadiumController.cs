using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Common.Constant;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.DTO.Accounts.Stadiums;
using StadiumEngine.Handlers.Queries.Accounts;
using StadiumEngine.Handlers.Queries.Accounts.Roles;
using StadiumEngine.WebUI.Infrastructure.Attributes;

namespace StadiumEngine.WebUI.Controllers.API.Accounts;

/// <summary>
/// Стадионы
/// </summary>
[Route("api/accounts/stadiums")]
public class StadiumController : BaseApiController
{
    /// <summary>
    /// Получить стадионы
    /// </summary>
    /// <returns></returns>
    [HttpGet("{roleId}")]
    [HasPermission(PermissionsKeys.GetStadiums)]
    public async Task<List<StadiumDto>> Get(int roleId)
    {
        var stadiums = await Mediator.Send(new GetStadiumsForRoleQuery(roleId));
        return stadiums;
    }
}