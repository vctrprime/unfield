using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Handlers.Queries.Accounts;
using StadiumEngine.Handlers.Queries.Accounts.Users;
using StadiumEngine.WebUI.Infrastructure.Attributes;

namespace StadiumEngine.WebUI.Controllers.API.Accounts;

/// <summary>
/// Разрешения пользователя
/// </summary>
[Route( "api/accounts/user-permissions" )]
public class UserPermissionController : BaseApiController
{
    /// <summary>
    /// Разрешения пользователя
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<UserPermissionDto>> Get()
    {
        var permissions = await Mediator.Send( new GetUserPermissionsQuery() );
        return permissions;
    }
}