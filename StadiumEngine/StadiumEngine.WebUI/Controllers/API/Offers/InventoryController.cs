using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Common.Constant;
using StadiumEngine.DTO.Offers.Inventories;
using StadiumEngine.Handlers.Commands.Offers.Inventories;
using StadiumEngine.Handlers.Queries.Offers.Inventories;
using StadiumEngine.WebUI.Infrastructure.Attributes;

namespace StadiumEngine.WebUI.Controllers.API.Offers;

/// <summary>
///     Инвентарь
/// </summary>
[Route( "api/offers/inventories" )]
public class InventoryController : BaseApiController
{
    /// <summary>
    ///     Получить инвентарь (список)
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [HasPermission( PermissionsKeys.GetInventories )]
    public async Task<List<InventoryDto>> GetAll()
    {
        var inventories = await Mediator.Send( new GetInventoriesQuery() );
        return inventories;
    }

    /// <summary>
    ///     Получить инвентарь
    /// </summary>
    /// <returns></returns>
    [HttpGet( "{inventoryId}" )]
    [HasPermission( PermissionsKeys.GetInventories )]
    public async Task<InventoryDto> Get( int inventoryId )
    {
        var inventory = await Mediator.Send( new GetInventoryQuery( inventoryId ) );
        return inventory;
    }

    /// <summary>
    ///     Добавить инвентарь
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [HasPermission( PermissionsKeys.InsertInventory )]
    public async Task<AddInventoryDto> Post( [FromForm] AddInventoryCommand command )
    {
        var dto = await Mediator.Send( command );
        return dto;
    }

    /// <summary>
    ///     Обновить инвентарь
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [HasPermission( PermissionsKeys.UpdateInventory )]
    public async Task<UpdateInventoryDto> Put( [FromForm] UpdateInventoryCommand command )
    {
        var dto = await Mediator.Send( command );
        return dto;
    }

    /// <summary>
    ///     Удалить инвентарь
    /// </summary>
    /// <returns></returns>
    [HttpDelete( "{inventoryId}" )]
    [HasPermission( PermissionsKeys.DeleteInventory )]
    public async Task<DeleteInventoryDto> Delete( int inventoryId )
    {
        var dto = await Mediator.Send( new DeleteInventoryCommand( inventoryId ) );
        return dto;
    }
}