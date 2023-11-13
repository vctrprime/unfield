using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO;
using StadiumEngine.Queries;

namespace StadiumEngine.Portal.Controllers.API;

/// <summary>
/// Информация о среде развертывания
/// </summary>
[Route( "api/env" )]
public class EnvDataController : BaseApiController
{
    /// <summary>
    /// Получить данные о среде развертывания
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<EnvDataDto> Get( [FromQuery] GetEnvDataQuery query )
    {
        EnvDataDto dto = await Mediator.Send( query );

        return dto;
    }
}