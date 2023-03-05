using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Common.Constant;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Handlers.Commands.Accounts.Users;
using StadiumEngine.Handlers.Queries.Accounts.Users;
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
    public async Task<List<UserDto>> Get()
    {
        List<UserDto> users = await Mediator.Send( new GetUsersQuery() );
        return users;
    }

    /// <summary>
    ///     Добавить пользователя
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [HasPermission( PermissionsKeys.InsertUser )]
    public async Task<AddUserDto> Post( AddUserCommand command )
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
    public async Task<UpdateUserDto> Put( UpdateUserCommand command )
    {
        UpdateUserDto dto = await Mediator.Send( command );
        return dto;
    }

    /// <summary>
    ///     Удалить пользователя
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpDelete( "{userId}" )]
    [HasPermission( PermissionsKeys.DeleteUser )]
    public async Task<DeleteUserDto> Delete( int userId )
    {
        DeleteUserDto dto = await Mediator.Send( new DeleteUserCommand( userId ) );
        return dto;
    }
}