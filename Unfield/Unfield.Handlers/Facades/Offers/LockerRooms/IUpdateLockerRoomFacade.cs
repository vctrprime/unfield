using Unfield.DTO.Offers.LockerRooms;
using Unfield.Commands.Offers.LockerRooms;

namespace Unfield.Handlers.Facades.Offers.LockerRooms;

internal interface IUpdateLockerRoomFacade
{
    Task<UpdateLockerRoomDto> UpdateAsync( UpdateLockerRoomCommand request, int stadiumId, int userId );
}