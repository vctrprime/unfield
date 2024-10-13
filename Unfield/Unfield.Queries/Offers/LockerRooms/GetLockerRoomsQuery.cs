using Mediator;
using Microsoft.AspNetCore.Mvc;
using Unfield.DTO.Offers.LockerRooms;

namespace Unfield.Queries.Offers.LockerRooms;

public sealed class GetLockerRoomsQuery : BaseQuery, IRequest<List<LockerRoomDto>>
{
    
}