using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Offers.Inventories;
using StadiumEngine.Handlers.Commands.Offers.Inventories;
using StadiumEngine.Handlers.Queries.Offers.Inventories;
using StadiumEngine.WebUI.Infrastructure.Attributes;

namespace StadiumEngine.WebUI.Controllers.API.Offers;

/// <summary>
/// Инвентарь
/// </summary>
[Route("api/offers/inventories")]
public class InventoryController : BaseApiController
{
    /// <summary>
    /// Получить инвентарь (список)
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [HasPermission("get-inventories")]
    public async Task<List<InventoryDto>> GetAll()
    {
        var inventories = await Mediator.Send(new GetInventoriesQuery());
        return inventories;
    }
    
    /// <summary>
    /// Получить инвентарь
    /// </summary>
    /// <returns></returns>
    [HttpGet("{inventoryId}")]
    [HasPermission("get-inventories")]
    public async Task<InventoryDto> Get(int inventoryId)
    {
        var inventory = await Mediator.Send(new GetInventoryQuery(inventoryId));
        return inventory;
    }
    
    /// <summary>
    /// Добавить инвентарь
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [HasPermission("insert-inventory")]
    public async Task<AddInventoryDto> Post([FromForm]AddInventoryCommand command)
    {
        var dto = await Mediator.Send(command);
        return dto;
    }
    
    /// <summary>
    /// Обновить инвентарь
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [HasPermission("update-inventory")]
    public async Task<UpdateInventoryDto> Put([FromForm]UpdateInventoryCommand command)
    {
        var dto = await Mediator.Send(command);
        return dto;
    }
    
    /// <summary>
    /// Удалить инвентарь
    /// </summary>
    /// <returns></returns>
    [HttpDelete("{inventoryId}")]
    [HasPermission("delete-inventory")]
    public async Task<DeleteInventoryDto> Delete(int inventoryId)
    {
        var dto = await Mediator.Send(new DeleteInventoryCommand(inventoryId));
        return dto;
    }
}