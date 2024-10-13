using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unfield.Common.Constant;
using Unfield.DTO.Accounts.Stadiums;
using Unfield.Extranet.Infrastructure.Attributes;
using Unfield.Queries.Accounts.Roles;
using Unfield.Queries.Accounts.Users;

namespace Unfield.Extranet.Controllers.API.Accounts;

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