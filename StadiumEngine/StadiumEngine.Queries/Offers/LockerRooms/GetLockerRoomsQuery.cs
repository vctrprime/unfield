using Mediator;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Offers.LockerRooms;

namespace StadiumEngine.Queries.Offers.LockerRooms;

public sealed class GetLockerRoomsQuery : BaseQuery, IRequest<List<LockerRoomDto>>
{
    
}