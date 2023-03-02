using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Rates.PriceGroups;
using StadiumEngine.Handlers.Commands.Rates.PriceGroups;
using StadiumEngine.Handlers.Queries.Rates;
using StadiumEngine.WebUI.Infrastructure.Attributes;

namespace StadiumEngine.WebUI.Controllers.API.Rates;

/// <summary>
/// Ценовые группы
/// </summary>
[Route("api/rates/price-groups")]
public class PriceGroupController : BaseApiController
{
    /// <summary>
    /// Получить ценовые группы
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [HasPermission("get-price-groups,get-fields")]
    public async Task<List<PriceGroupDto>> GetAll()
    {
        var priceGroups = await Mediator.Send(new GetPriceGroupsQuery());
        return priceGroups;
    }
    
    /// <summary>
    /// Получить ценовую группу
    /// </summary>
    /// <returns></returns>
    [HttpGet("{priceGroupId}")]
    [HasPermission("get-price-groups")]
    public async Task<PriceGroupDto> Get(int priceGroupId)
    {
        var priceGroup = await Mediator.Send(new GetPriceGroupQuery(priceGroupId));
        return priceGroup;
    }
    
    /// <summary>
    /// Добавить ценовую группу
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [HasPermission("insert-price-group")]
    public async Task<AddPriceGroupDto> Post(AddPriceGroupCommand command)
    {
        var dto = await Mediator.Send(command);
        return dto;
    }
    
    /// <summary>
    /// Обновить ценовую группу
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [HasPermission("update-price-group")]
    public async Task<UpdatePriceGroupDto> Put(UpdatePriceGroupCommand command)
    {
        var dto = await Mediator.Send(command);
        return dto;
    }
    
    /// <summary>
    /// Удалить ценовую группу
    /// </summary>
    /// <returns></returns>
    [HttpDelete("{priceGroupId}")]
    [HasPermission("delete-price-group")]
    public async Task<DeletePriceGroupDto> Delete(int priceGroupId)
    {
        var dto = await Mediator.Send(new DeletePriceGroupCommand(priceGroupId));
        return dto;
    }
    
    
}