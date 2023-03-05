using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Common.Constant;
using StadiumEngine.DTO.Rates.Tariffs;
using StadiumEngine.Handlers.Commands.Rates.Tariffs;
using StadiumEngine.Handlers.Queries.Rates.Tariffs;
using StadiumEngine.WebUI.Infrastructure.Attributes;

namespace StadiumEngine.WebUI.Controllers.API.Rates;

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
    [HasPermission( PermissionsKeys.GetTariffs )]
    public async Task<List<TariffDto>> GetAll()
    {
        List<TariffDto> tariffs = await Mediator.Send( new GetTariffsQuery() );
        return tariffs;
    }

    /// <summary>
    ///     Получить тариф
    /// </summary>
    /// <returns></returns>
    [HttpGet( "{tariffId}" )]
    [HasPermission( PermissionsKeys.GetTariffs )]
    public async Task<TariffDto> Get( int tariffId )
    {
        TariffDto tariff = await Mediator.Send( new GetTariffQuery( tariffId ) );
        return tariff;
    }

    /// <summary>
    ///     Добавить тариф
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [HasPermission( PermissionsKeys.InsertTariff )]
    public async Task<AddTariffDto> Post( AddTariffCommand command )
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
    public async Task<UpdateTariffDto> Put( UpdateTariffCommand command )
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
    public async Task<DeleteTariffDto> Delete( int tariffId )
    {
        DeleteTariffDto dto = await Mediator.Send( new DeleteTariffCommand( tariffId ) );
        return dto;
    }
}