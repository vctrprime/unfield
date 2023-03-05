using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Common.Constant;
using StadiumEngine.DTO.Rates.PriceGroups;
using StadiumEngine.Handlers.Commands.Rates.PriceGroups;
using StadiumEngine.Handlers.Queries.Rates;
using StadiumEngine.WebUI.Infrastructure.Attributes;

namespace StadiumEngine.WebUI.Controllers.API.Rates;

/// <summary>
///     Ценовые группы
/// </summary>
[Route( "api/rates/price-groups" )]
public class PriceGroupController : BaseApiController
{
    /// <summary>
    ///     Получить ценовые группы
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [HasPermission( $"{PermissionsKeys.GetPriceGroups},{PermissionsKeys.GetFields}" )]
    public async Task<List<PriceGroupDto>> GetAll()
    {
        List<PriceGroupDto> priceGroups = await Mediator.Send( new GetPriceGroupsQuery() );
        return priceGroups;
    }

    /// <summary>
    ///     Получить ценовую группу
    /// </summary>
    /// <returns></returns>
    [HttpGet( "{priceGroupId}" )]
    [HasPermission( PermissionsKeys.GetPriceGroups )]
    public async Task<PriceGroupDto> Get( int priceGroupId )
    {
        PriceGroupDto priceGroup = await Mediator.Send( new GetPriceGroupQuery( priceGroupId ) );
        return priceGroup;
    }

    /// <summary>
    ///     Добавить ценовую группу
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [HasPermission( PermissionsKeys.InsertPriceGroup )]
    public async Task<AddPriceGroupDto> Post( AddPriceGroupCommand command )
    {
        AddPriceGroupDto dto = await Mediator.Send( command );
        return dto;
    }

    /// <summary>
    ///     Обновить ценовую группу
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [HasPermission( PermissionsKeys.UpdatePriceGroup )]
    public async Task<UpdatePriceGroupDto> Put( UpdatePriceGroupCommand command )
    {
        UpdatePriceGroupDto dto = await Mediator.Send( command );
        return dto;
    }

    /// <summary>
    ///     Удалить ценовую группу
    /// </summary>
    /// <returns></returns>
    [HttpDelete( "{priceGroupId}" )]
    [HasPermission( PermissionsKeys.DeletePriceGroup )]
    public async Task<DeletePriceGroupDto> Delete( int priceGroupId )
    {
        DeletePriceGroupDto dto = await Mediator.Send( new DeletePriceGroupCommand( priceGroupId ) );
        return dto;
    }
}