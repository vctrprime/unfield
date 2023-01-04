using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Queries.Accounts;

namespace StadiumEngine.WebUI.Controllers.API.Accounts;

/// <summary>
/// Стадионы пользователя
/// </summary>
[Route("api/account/stadiums")]
public class UserStadiumController : BaseApiController
{
    /// <summary>
    /// Стадионы пользователя
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<UserStadiumDto>> Get()
    {
        var stadiums = await Mediator.Send(new GetUserStadiumsQuery());
        return stadiums;
    }
}