using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Handlers.Queries.Accounts.Users;

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
    public async Task<List<UserStadiumDto>> Get()
    {
        List<UserStadiumDto> stadiums = await Mediator.Send( new GetUserStadiumsQuery() );
        return stadiums;
    }
}