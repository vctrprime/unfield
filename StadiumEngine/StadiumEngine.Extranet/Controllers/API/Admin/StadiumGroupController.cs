using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Admin;
using StadiumEngine.Extranet.Infrastructure.Attributes;
using StadiumEngine.Queries.Admin;

namespace StadiumEngine.Extranet.Controllers.API.Admin;

/// <summary>
///     Группы стадионов
/// </summary>
[Route( "api/admin/stadium-groups" )]
public class StadiumGroupController : BaseApiController
{
    /// <summary>
    ///     Получить организации
    /// </summary>
    /// <returns></returns>
    [AdminFeature]
    [HttpGet]
    public async Task<List<StadiumGroupDto>> Get( [FromQuery] GetStadiumGroupsByFilterQuery query )
    {
        List<StadiumGroupDto> stadiumGroups = await Mediator.Send( query );
        return stadiumGroups;
    }
}