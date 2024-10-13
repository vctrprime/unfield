using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unfield.DTO.Geo;
using Unfield.Queries.Geo;

namespace Unfield.BookingForm.Controllers.API.Geo;

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