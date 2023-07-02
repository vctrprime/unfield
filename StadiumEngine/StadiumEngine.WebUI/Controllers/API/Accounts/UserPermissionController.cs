using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Queries.Accounts.Users;

namespace StadiumEngine.WebUI.Controllers.API.Accounts;

/// <summary>
///     Разрешения пользователя
/// </summary>
[Route( "api/accounts/user-permissions" )]
public class UserPermissionController : BaseApiController
{
    /// <summary>
    ///     Разрешения пользователя
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<UserPermissionDto>> Get( [FromRoute] GetUserPermissionsQuery query )
    {
        List<UserPermissionDto> permissions = await Mediator.Send( query );
        return permissions;
    }
}