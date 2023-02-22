using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Offers.LockerRooms;
using StadiumEngine.Handlers.Commands.Offers.LockerRooms;
using StadiumEngine.Handlers.Queries.Offers.LockerRooms;
using StadiumEngine.WebUI.Infrastructure.Attributes;

namespace StadiumEngine.WebUI.Controllers.API.Offers;

/// <summary>
/// Раздевалки
/// </summary>
[Route("api/offers/locker-rooms")]
public class LockerRoomController : BaseApiController
{
    /// <summary>
    /// Получить раздевалки
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [HasPermission("get-locker-rooms")]
    public async Task<List<LockerRoomDto>> GetAll()
    {
        var lockerRooms = await Mediator.Send(new GetLockerRoomsQuery());
        return lockerRooms;
    }
    
    /// <summary>
    /// Получить раздевалку
    /// </summary>
    /// <returns></returns>
    [HttpGet("{lockerRoomId}")]
    [HasPermission("get-locker-rooms")]
    public async Task<LockerRoomDto> Get(int lockerRoomId)
    {
        var lockerRoom = await Mediator.Send(new GetLockerRoomQuery(lockerRoomId));
        return lockerRoom;
    }
    
    /// <summary>
    /// Добавить раздевалку
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [HasPermission("insert-locker-room")]
    public async Task<AddLockerRoomDto> Post(AddLockerRoomCommand command)
    {
        var dto = await Mediator.Send(command);
        return dto;
    }
    
    /// <summary>
    /// Обновить раздевалку
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [HasPermission("update-locker-room")]
    public async Task<UpdateLockerRoomDto> Put(UpdateLockerRoomCommand command)
    {
        var dto = await Mediator.Send(command);
        return dto;
    }
    
    /// <summary>
    /// Удалить раздевалку
    /// </summary>
    /// <returns></returns>
    [HttpDelete("{lockerRoomId}")]
    [HasPermission("delete-locker-room")]
    public async Task<DeleteLockerRoomDto> Delete(int lockerRoomId)
    {
        var dto = await Mediator.Send(new DeleteLockerRoomCommand(lockerRoomId));
        return dto;
    }
}