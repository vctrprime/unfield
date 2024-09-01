using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Common.Constant;
using StadiumEngine.DTO.Rates.PriceGroups;
using StadiumEngine.Commands.Rates.PriceGroups;
using StadiumEngine.Commands.Rates.Prices;
using StadiumEngine.DTO.Rates.Prices;
using StadiumEngine.Extranet.Infrastructure.Attributes;
using StadiumEngine.Queries.Rates.PriceGroups;
using StadiumEngine.Queries.Rates.Prices;

namespace StadiumEngine.Extranet.Controllers.API.Rates;

/// <summary>
///  Цены
/// </summary>
[Route( "api/rates/prices" )]
public class PriceController : BaseApiController
{
    /// <summary>
    ///     Получить цены
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [HasPermission( $"{PermissionsKeys.GetPrices}" )]
    public async Task<List<PriceDto>> GetAll( [FromRoute] GetPricesQuery query)
    {
        List<PriceDto> prices = await Mediator.Send( query );
        return prices;
    }
    
    /// <summary>
    ///     Установить цены
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [HasPermission( PermissionsKeys.SetPrices )]
    public async Task<SetPricesDto> Put( [FromBody] SetPricesCommand command )
    {
        SetPricesDto dto = await Mediator.Send( command );
        return dto;
    }
    
}