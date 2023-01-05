using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Queries.Accounts;
using StadiumEngine.WebUI.Infrastructure.Attributes;

namespace StadiumEngine.WebUI.Controllers.API.Accounts;

/// <summary>
/// Роли
/// </summary>
[Route("api/accounts/roles")]
public class RoleController : BaseApiController
{
    /// <summary>
    /// Получить роли
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [HasPermission("get-roles")]
    public async Task<List<RoleDto>> Get()
    {
        var roles = await Mediator.Send(new GetRolesQuery());
        return roles;
    }
}