using Mediator;
using StadiumEngine.DTO.Offers.LockerRooms;

namespace StadiumEngine.Queries.Offers.LockerRooms;

public sealed class GetLockerRoomQuery : BaseQuery, IRequest<LockerRoomDto>
{
    public int LockerRoomId { get; set; }
}