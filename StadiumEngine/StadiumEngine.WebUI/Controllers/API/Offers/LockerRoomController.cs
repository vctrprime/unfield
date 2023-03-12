using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Common.Constant;
using StadiumEngine.DTO.Offers.LockerRooms;
using StadiumEngine.Commands.Offers.LockerRooms;
using StadiumEngine.Queries.Offers.LockerRooms;
using StadiumEngine.WebUI.Infrastructure.Attributes;

namespace StadiumEngine.WebUI.Controllers.API.Offers;

/// <summary>
///     Раздевалки
/// </summary>
[Route( "api/offers/locker-rooms" )]
public class LockerRoomController : BaseApiController
{
    /// <summary>
    ///     Получить раздевалки
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [HasPermission( PermissionsKeys.GetLockerRooms )]
    public async Task<List<LockerRoomDto>> GetAll()
    {
        List<LockerRoomDto> lockerRooms = await Mediator.Send( new GetLockerRoomsQuery() );
        return lockerRooms;
    }

    /// <summary>
    ///     Получить раздевалку
    /// </summary>
    /// <returns></returns>
    [HttpGet( "{lockerRoomId}" )]
    [HasPermission( PermissionsKeys.GetLockerRooms )]
    public async Task<LockerRoomDto> Get( int lockerRoomId )
    {
        LockerRoomDto lockerRoom = await Mediator.Send( new GetLockerRoomQuery( lockerRoomId ) );
        return lockerRoom;
    }

    /// <summary>
    ///     Добавить раздевалку
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [HasPermission( PermissionsKeys.InsertLockerRoom )]
    public async Task<AddLockerRoomDto> Post( AddLockerRoomCommand command )
    {
        AddLockerRoomDto dto = await Mediator.Send( command );
        return dto;
    }

    /// <summary>
    ///     Обновить раздевалку
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [HasPermission( PermissionsKeys.UpdateLockerRoom )]
    public async Task<UpdateLockerRoomDto> Put( UpdateLockerRoomCommand command )
    {
        UpdateLockerRoomDto dto = await Mediator.Send( command );
        return dto;
    }

    /// <summary>
    ///     Удалить раздевалку
    /// </summary>
    /// <returns></returns>
    [HttpDelete( "{lockerRoomId}" )]
    [HasPermission( PermissionsKeys.DeleteLockerRoom )]
    public async Task<DeleteLockerRoomDto> Delete( int lockerRoomId )
    {
        DeleteLockerRoomDto dto = await Mediator.Send( new DeleteLockerRoomCommand( lockerRoomId ) );
        return dto;
    }
}