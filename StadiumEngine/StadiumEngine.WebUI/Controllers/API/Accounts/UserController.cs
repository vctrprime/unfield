using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Common.Constant;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Commands.Accounts.Users;
using StadiumEngine.Queries.Accounts.Users;
using StadiumEngine.WebUI.Infrastructure.Attributes;

namespace StadiumEngine.WebUI.Controllers.API.Accounts;

/// <summary>
///     Пользователи
/// </summary>
[Route( "api/accounts/users" )]
public class UserController : BaseApiController
{
    /// <summary>
    ///     Получить пользователей
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [HasPermission( PermissionsKeys.GetUsers )]
    public async Task<List<UserDto>> Get( [FromRoute] GetUsersQuery query )
    {
        List<UserDto> users = await Mediator.Send( query );
        return users;
    }

    /// <summary>
    ///     Добавить пользователя
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [HasPermission( PermissionsKeys.InsertUser )]
    public async Task<AddUserDto> Post( [FromBody] AddUserCommand command )
    {
        AddUserDto dto = await Mediator.Send( command );
        return dto;
    }

    /// <summary>
    ///     Изменить пользователя
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    [HasPermission( PermissionsKeys.UpdateUser )]
    public async Task<UpdateUserDto> Put( [FromBody] UpdateUserCommand command )
    {
        UpdateUserDto dto = await Mediator.Send( command );
        return dto;
    }

    /// <summary>
    ///     Удалить пользователя
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpDelete( "{userId}" )]
    [HasPermission( PermissionsKeys.DeleteUser )]
    public async Task<DeleteUserDto> Delete( [FromRoute] DeleteUserCommand command )
    {
        DeleteUserDto dto = await Mediator.Send( command );
        return dto;
    }
}