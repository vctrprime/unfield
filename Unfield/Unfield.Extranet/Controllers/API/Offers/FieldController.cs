using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unfield.Common.Constant;
using Unfield.DTO.Offers.Fields;
using Unfield.Commands.Offers.Fields;
using Unfield.Extranet.Infrastructure.Attributes;
using Unfield.Queries.Offers.Fields;

namespace Unfield.Extranet.Controllers.API.Offers;

/// <summary>
///     Площадки
/// </summary>
[Route( "api/offers/fields" )]
public class FieldController : BaseApiController
{
    /// <summary>
    ///     Получить площадки
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [HasPermission( $"{PermissionsKeys.GetFields},{PermissionsKeys.GetPrices},{PermissionsKeys.GetBreaks}" )]
    public async Task<List<FieldDto>> GetAll( [FromRoute] GetFieldsQuery query )
    {
        List<FieldDto> fields = await Mediator.Send( query );
        return fields;
    }

    /// <summary>
    ///     Получить площадку
    /// </summary>
    /// <returns></returns>
    [HttpGet( "{fieldId}" )]
    [HasPermission( PermissionsKeys.GetFields )]
    public async Task<FieldDto> Get( [FromRoute] GetFieldQuery query )
    {
        FieldDto field = await Mediator.Send( query );
        return field;
    }

    /// <summary>
    ///     Добавить площадку
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [HasPermission( PermissionsKeys.InsertField )]
    public async Task<AddFieldDto> Post( [FromForm] AddFieldCommand command )
    {
        AddFieldDto dto = await Mediator.Send( command );
        return dto;
    }

    /// <summary>
    ///     Обновить площадку
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [HasPermission( PermissionsKeys.UpdateField )]
    public async Task<UpdateFieldDto> Put( [FromForm] UpdateFieldCommand command )
    {
        UpdateFieldDto dto = await Mediator.Send( command );
        return dto;
    }

    /// <summary>
    ///     Удалить площадку
    /// </summary>
    /// <returns></returns>
    [HttpDelete( "{fieldId}" )]
    [HasPermission( PermissionsKeys.DeleteField )]
    public async Task<DeleteFieldDto> Delete( [FromRoute] DeleteFieldCommand command )
    {
        DeleteFieldDto dto = await Mediator.Send( command );
        return dto;
    }
}