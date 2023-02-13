using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Offers.Fields;
using StadiumEngine.Handlers.Commands.Offers.Fields;
using StadiumEngine.Handlers.Queries.Offers.Fields;
using StadiumEngine.WebUI.Infrastructure.Attributes;

namespace StadiumEngine.WebUI.Controllers.API.Offers;

/// <summary>
/// Площадки
/// </summary>
[Route("api/offers/fields")]
public class FieldController : BaseApiController
{
    /// <summary>
    /// Получить площадки
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [HasPermission("get-fields")]
    public async Task<List<FieldDto>> GetAll()
    {
        var fields = await Mediator.Send(new GetFieldsQuery());
        return fields;
    }
    
    /// <summary>
    /// Получить площадку
    /// </summary>
    /// <returns></returns>
    [HttpGet("{fieldId}")]
    [HasPermission("get-fields")]
    public async Task<FieldDto> Get(int fieldId)
    {
        var field = await Mediator.Send(new GetFieldQuery(fieldId));
        return field;
    }
    
    /// <summary>
    /// Добавить площадку
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [HasPermission("insert-field")]
    public async Task<AddFieldDto> Post([FromForm]AddFieldCommand command)
    {
        var dto = await Mediator.Send(command);
        return dto;
    }
    
    /// <summary>
    /// Обновить площадку
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [HasPermission("update-field")]
    public async Task<UpdateFieldDto> Put([FromForm]UpdateFieldCommand command)
    {
        var dto = await Mediator.Send(command);
        return dto;
    }
    
    /// <summary>
    /// Удалить площадку
    /// </summary>
    /// <returns></returns>
    [HttpDelete("{fieldId}")]
    [HasPermission("delete-field")]
    public async Task<DeleteFieldDto> Delete(int fieldId)
    {
        var dto = await Mediator.Send(new DeleteFieldCommand(fieldId));
        return dto;
    }
}