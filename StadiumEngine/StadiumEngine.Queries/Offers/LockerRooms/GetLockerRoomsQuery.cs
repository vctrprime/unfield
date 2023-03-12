using Mediator;
using StadiumEngine.DTO.Offers.LockerRooms;

namespace StadiumEngine.Queries.Offers.LockerRooms;

public sealed class GetLockerRoomsQuery : IRequest<List<LockerRoomDto>>
{
}