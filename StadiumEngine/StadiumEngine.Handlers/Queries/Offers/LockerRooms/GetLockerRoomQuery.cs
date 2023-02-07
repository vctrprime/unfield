using Mediator;
using StadiumEngine.DTO.Offers.LockerRooms;

namespace StadiumEngine.Handlers.Queries.Offers.LockerRooms;

public sealed class GetLockerRoomQuery : IRequest<LockerRoomDto>
{
    public GetLockerRoomQuery(int lockerRoomId)
    {
        LockerRoomId = lockerRoomId;
    }
    
    public int LockerRoomId { get; }
}