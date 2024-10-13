using Mediator;
using Unfield.DTO.Offers.LockerRooms;

namespace Unfield.Queries.Offers.LockerRooms;

public sealed class GetLockerRoomQuery : BaseQuery, IRequest<LockerRoomDto>
{
    public int LockerRoomId { get; set; }
}