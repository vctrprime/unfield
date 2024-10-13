using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unfield.Commands.Accounts.Users;
using Unfield.Common.Constant;
using Unfield.DTO.Accounts.Users;
using Unfield.Extranet.Infrastructure.Attributes;
using Unfield.Queries.Accounts.Users;

namespace Unfield.Extranet.Controllers.API.Accounts;

/// <summary>
///     Стадионы пользователя
/// </summary>
[Route( "api/accounts/user-stadiums" )]
public class UserStadiumController : BaseApiController
{
    /// <summary>
    ///     Стадионы пользователя
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<UserStadiumDto>> Get( [FromRoute] GetUserStadiumsQuery query )
    {
        List<UserStadiumDto> stadiums = await Mediator.Send( query );
        return stadiums;
    }
    
    /// <summary>
    ///     Добавить/убрать связь
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [HasPermission( PermissionsKeys.ToggleUserStadium )]
    public async Task<ToggleUserStadiumDto> Post( [FromBody] ToggleUserStadiumCommand command )
    {
        ToggleUserStadiumDto dto = await Mediator.Send( command );
        return dto;
    }
}