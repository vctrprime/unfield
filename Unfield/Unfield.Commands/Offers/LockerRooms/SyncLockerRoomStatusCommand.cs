using Mediator;
using Unfield.DTO.Offers.LockerRooms;

namespace Unfield.Commands.Offers.LockerRooms;

public sealed class SyncLockerRoomStatusCommand : BaseCommand, IRequest<SyncLockerRoomStatusDto>
{
    public int LockerRoomId { get; set; }
}