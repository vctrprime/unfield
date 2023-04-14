using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Geo;
using StadiumEngine.Queries.Geo;

namespace StadiumEngine.WebUI.Controllers.API.Geo;

/// <summary>
///     Города
/// </summary>
[Route( "api/geo/cities" )]
[AllowAnonymous]
public class CityController : BaseApiController
{
    /// <summary>
    ///     Получить данные по городам
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<CityDto>> Get( string q )
    {
        List<CityDto> cities = await Mediator.Send( new GetCitiesQuery( q ?? String.Empty ));
        
        return cities;
    }
}