using Mediator;
using StadiumEngine.DTO.Offers.LockerRooms;

namespace StadiumEngine.Commands.Offers.LockerRooms;

public sealed class SyncLockerRoomStatusCommand : BaseCommand, IRequest<SyncLockerRoomStatusDto>
{
    public int LockerRoomId { get; set; }
}