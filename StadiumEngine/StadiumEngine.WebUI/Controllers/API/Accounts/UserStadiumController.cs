using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Queries.Accounts.Users;

namespace StadiumEngine.WebUI.Controllers.API.Accounts;

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
}