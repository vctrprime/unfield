using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unfield.Common.Constant;
using Unfield.DTO.Rates.PriceGroups;
using Unfield.Commands.Rates.PriceGroups;
using Unfield.Commands.Rates.Prices;
using Unfield.DTO.Rates.Prices;
using Unfield.Extranet.Infrastructure.Attributes;
using Unfield.Queries.Rates.PriceGroups;
using Unfield.Queries.Rates.Prices;

namespace Unfield.Extranet.Controllers.API.Rates;

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