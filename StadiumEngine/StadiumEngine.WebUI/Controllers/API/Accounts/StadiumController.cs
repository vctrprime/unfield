using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Queries.Accounts;
using StadiumEngine.WebUI.Infrastructure.Attributes;

namespace StadiumEngine.WebUI.Controllers.API.Accounts;

/// <summary>
/// Стадионы
/// </summary>
[Route("api/accounts/stadiums")]
public class StadiumController : BaseApiController
{
    /// <summary>
    /// Получить стадионы
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [HasPermission("get-stadiums")]
    public async Task<List<StadiumDto>> Get()
    {
        var stadiums = await Mediator.Send(new GetStadiumsQuery());
        return stadiums;
    }
}