using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Queries.Accounts;
using StadiumEngine.WebUI.Infrastructure.Attributes;

namespace StadiumEngine.WebUI.Controllers.API.Accounts;

/// <summary>
/// Разрешения
/// </summary>
[Route("api/permissions")]
public class PermissionController : BaseApiController
{
    /// <summary>
    /// Получить разрешения для роли
    /// </summary>
    /// <returns></returns>
    [HttpGet("{roleId}")]
    [HasPermission("get-roles")]
    public async Task<List<PermissionDto>> Get(int roleId)
    {
        var permissions = await Mediator.Send(new GetPermissionsForRoleQuery(roleId));
        return permissions;
    }
}