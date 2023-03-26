using StadiumEngine.DTO.Offers.LockerRooms;
using StadiumEngine.Commands.Offers.LockerRooms;

namespace StadiumEngine.Handlers.Facades.Offers.LockerRooms;

internal interface IUpdateLockerRoomFacade
{
    Task<UpdateLockerRoomDto> UpdateAsync( UpdateLockerRoomCommand request, int stadiumId, int userId );
}