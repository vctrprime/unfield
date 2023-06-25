using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Settings.Breaks;
using StadiumEngine.Queries.Settings.Breaks;

namespace StadiumEngine.WebUI.Controllers.API.Settings;

/// <summary>
///     Перерывы
/// </summary>
[Route( "api/settings/breaks" )]
public class BreakController : BaseApiController
{
    /// <summary>
    ///     Получить перерывы
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<BreakDto>> GetAll()
    {
        List<BreakDto> breaks = await Mediator.Send( new GetBreaksQuery() );
        return breaks;
    }
}