using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Common.Constant;
using StadiumEngine.DTO.Offers.LockerRooms;
using StadiumEngine.Commands.Offers.LockerRooms;
using StadiumEngine.Extranet.Infrastructure.Attributes;
using StadiumEngine.Queries.Offers.LockerRooms;

namespace StadiumEngine.Extranet.Controllers.API.Offers;

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
    public async Task<List<LockerRoomDto>> GetAll( [FromRoute] GetLockerRoomsQuery query )
    {
        List<LockerRoomDto> lockerRooms = await Mediator.Send( query );
        return lockerRooms;
    }

    /// <summary>
    ///     Получить раздевалку
    /// </summary>
    /// <returns></returns>
    [HttpGet( "{lockerRoomId}" )]
    [HasPermission( PermissionsKeys.GetLockerRooms )]
    public async Task<LockerRoomDto> Get( [FromRoute] GetLockerRoomQuery query )
    {
        LockerRoomDto lockerRoom = await Mediator.Send( query );
        return lockerRoom;
    }

    /// <summary>
    ///     Добавить раздевалку
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [HasPermission( PermissionsKeys.InsertLockerRoom )]
    public async Task<AddLockerRoomDto> Post( [FromBody] AddLockerRoomCommand command )
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
    public async Task<UpdateLockerRoomDto> Put( [FromBody] UpdateLockerRoomCommand command )
    {
        UpdateLockerRoomDto dto = await Mediator.Send( command );
        return dto;
    }
    
    /// <summary>
    ///     Синхронизировать статус раздевалки
    /// </summary>
    /// <returns></returns>
    [HttpPut("{lockerRoomId}/status/sync")]
    [HasPermission( PermissionsKeys.SyncLockerRoomStatus )]
    public async Task<SyncLockerRoomStatusDto> SyncStatus( [FromRoute] SyncLockerRoomStatusCommand command )
    {
        SyncLockerRoomStatusDto dto = await Mediator.Send( command );
        return dto;
    }

    /// <summary>
    ///     Удалить раздевалку
    /// </summary>
    /// <returns></returns>
    [HttpDelete( "{lockerRoomId}" )]
    [HasPermission( PermissionsKeys.DeleteLockerRoom )]
    public async Task<DeleteLockerRoomDto> Delete( [FromRoute] DeleteLockerRoomCommand command )
    {
        DeleteLockerRoomDto dto = await Mediator.Send( command );
        return dto;
    }
}