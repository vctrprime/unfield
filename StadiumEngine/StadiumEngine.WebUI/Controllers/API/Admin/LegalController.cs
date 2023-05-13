using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Admin;
using StadiumEngine.Queries.Admin;
using StadiumEngine.WebUI.Infrastructure.Attributes;

namespace StadiumEngine.WebUI.Controllers.API.Admin;

/// <summary>
///     Организации
/// </summary>
[Route( "api/admin/legals" )]
public class LegalController : BaseApiController
{
    /// <summary>
    ///     Получить организации
    /// </summary>
    /// <returns></returns>
    [AdminFeature]
    [HttpGet]
    public async Task<List<LegalDto>> Get( [FromQuery] GetLegalsByFilterQuery query )
    {
        List<LegalDto> legals = await Mediator.Send( query );
        return legals;
    }
}