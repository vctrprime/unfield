using Mediator;
using StadiumEngine.DTO.Offers.LockerRooms;

namespace StadiumEngine.Commands.Offers.LockerRooms;

public sealed class DeleteLockerRoomCommand : IRequest<DeleteLockerRoomDto>
{
    public int LockerRoomId { get; set; }
}