using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Common.Configuration;
using StadiumEngine.DTO;

namespace StadiumEngine.Portal.Controllers.API;

/// <summary>
/// Информация о среде развертывания
/// </summary>
[Route( "api/env" )]
public class EnvDataController : BaseApiController
{
    private readonly EnvConfig _config;

    public EnvDataController( EnvConfig config )
    {
        _config = config;
    }
    
    /// <summary>
    /// Получить данные о среде развертывания
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<EnvDataDto> Get()
    {
        EnvDataDto dto = new EnvDataDto
        {
            ExtranetHost = _config.ExtranetHost,
            PortalHost = _config.PortalHost
        };

        return dto;
    }
}