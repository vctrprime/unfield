using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.DTO.Admin;
using StadiumEngine.Handlers.Queries.Admin;
using StadiumEngine.WebUI.Infrastructure.Attributes;

namespace StadiumEngine.WebUI.Controllers.API.Admin;

/// <summary>
/// Организации
/// </summary>
[Route("api/admin/legals")]
public class LegalController : BaseApiController
{
    /// <summary>
    /// Получить организации
    /// </summary>
    /// <returns></returns>
    [AdminFeature]
    [HttpGet]
    public async Task<List<LegalDto>> Get(string q)
    {
        var legals = await Mediator.Send(new GetLegalsByFilterQuery(q ?? string.Empty));
        return legals;
    }
}