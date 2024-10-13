using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unfield.Common.Constant;
using Unfield.DTO.Rates.PriceGroups;
using Unfield.Commands.Rates.PriceGroups;
using Unfield.Extranet.Infrastructure.Attributes;
using Unfield.Queries.Rates.PriceGroups;

namespace Unfield.Extranet.Controllers.API.Rates;

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
    public async Task<List<PriceGroupDto>> GetAll( [FromRoute] GetPriceGroupsQuery query )
    {
        List<PriceGroupDto> priceGroups = await Mediator.Send( query );
        return priceGroups;
    }

    /// <summary>
    ///     Получить ценовую группу
    /// </summary>
    /// <returns></returns>
    [HttpGet( "{priceGroupId}" )]
    [HasPermission( PermissionsKeys.GetPriceGroups )]
    public async Task<PriceGroupDto> Get( [FromRoute] GetPriceGroupQuery query )
    {
        PriceGroupDto priceGroup = await Mediator.Send( query );
        return priceGroup;
    }

    /// <summary>
    ///     Добавить ценовую группу
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [HasPermission( PermissionsKeys.InsertPriceGroup )]
    public async Task<AddPriceGroupDto> Post( [FromBody] AddPriceGroupCommand command )
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
    public async Task<UpdatePriceGroupDto> Put( [FromBody] UpdatePriceGroupCommand command )
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
    public async Task<DeletePriceGroupDto> Delete( [FromRoute] DeletePriceGroupCommand command )
    {
        DeletePriceGroupDto dto = await Mediator.Send( command );
        return dto;
    }
}