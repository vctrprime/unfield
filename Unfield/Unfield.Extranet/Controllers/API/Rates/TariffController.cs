using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unfield.Common.Constant;
using Unfield.DTO.Rates.Tariffs;
using Unfield.Commands.Rates.Tariffs;
using Unfield.Extranet.Infrastructure.Attributes;
using Unfield.Queries.Rates.Tariffs;

namespace Unfield.Extranet.Controllers.API.Rates;

/// <summary>
///     Тарифы
/// </summary>
[Route( "api/rates/tariffs" )]
public class TariffController : BaseApiController
{
    /// <summary>
    ///     Получить тарифы
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [HasPermission( $"{PermissionsKeys.GetTariffs},{PermissionsKeys.GetPrices}" )]
    public async Task<List<TariffDto>> GetAll( [FromRoute] GetTariffsQuery query )
    {
        List<TariffDto> tariffs = await Mediator.Send( query );
        return tariffs;
    }

    /// <summary>
    ///     Получить тариф
    /// </summary>
    /// <returns></returns>
    [HttpGet( "{tariffId}" )]
    [HasPermission( PermissionsKeys.GetTariffs )]
    public async Task<TariffDto> Get( [FromRoute] GetTariffQuery query )
    {
        TariffDto tariff = await Mediator.Send( query );
        return tariff;
    }

    /// <summary>
    ///     Добавить тариф
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [HasPermission( PermissionsKeys.InsertTariff )]
    public async Task<AddTariffDto> Post( [FromBody] AddTariffCommand command )
    {
        AddTariffDto dto = await Mediator.Send( command );
        return dto;
    }

    /// <summary>
    ///     Обновить тариф
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [HasPermission( PermissionsKeys.UpdateTariff )]
    public async Task<UpdateTariffDto> Put( [FromBody] UpdateTariffCommand command )
    {
        UpdateTariffDto dto = await Mediator.Send( command );
        return dto;
    }

    /// <summary>
    ///     Удалить тариф
    /// </summary>
    /// <returns></returns>
    [HttpDelete( "{tariffId}" )]
    [HasPermission( PermissionsKeys.DeleteTariff )]
    public async Task<DeleteTariffDto> Delete( [FromRoute] DeleteTariffCommand command )
    {
        DeleteTariffDto dto = await Mediator.Send( command );
        return dto;
    }
}