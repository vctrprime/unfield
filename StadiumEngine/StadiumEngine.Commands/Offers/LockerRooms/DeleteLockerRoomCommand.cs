using Mediator;
using StadiumEngine.DTO.Offers.LockerRooms;

namespace StadiumEngine.Commands.Offers.LockerRooms;

public sealed class DeleteLockerRoomCommand : IRequest<DeleteLockerRoomDto>
{
    public DeleteLockerRoomCommand( int lockerRoomId )
    {
        LockerRoomId = lockerRoomId;
    }

    public int LockerRoomId { get; }
}