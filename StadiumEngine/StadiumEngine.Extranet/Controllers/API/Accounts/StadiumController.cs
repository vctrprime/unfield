using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Common.Constant;
using StadiumEngine.DTO.Accounts.Stadiums;
using StadiumEngine.Extranet.Infrastructure.Attributes;
using StadiumEngine.Queries.Accounts.Roles;
using StadiumEngine.Queries.Accounts.Users;

namespace StadiumEngine.Extranet.Controllers.API.Accounts;

/// <summary>
///     Стадионы
/// </summary>
[Route( "api/accounts/stadiums" )]
public class StadiumController : BaseApiController
{
    /// <summary>
    ///     Получить стадионы
    /// </summary>
    /// <returns></returns>
    [HttpGet( "{userId}" )]
    [HasPermission( PermissionsKeys.GetStadiums )]
    public async Task<List<StadiumDto>> Get( [FromRoute] GetStadiumsForUserQuery query )
    {
        List<StadiumDto> stadiums = await Mediator.Send( query );
        return stadiums;
    }
}