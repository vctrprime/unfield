using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Geo;
using StadiumEngine.Queries.Geo;

namespace StadiumEngine.BookingForm.Controllers.API;

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
    public async Task<List<CityDto>> Get( [FromQuery] GetCitiesQuery query )
    {
        List<CityDto> cities = await Mediator.Send( query );

        return cities;
    }
}