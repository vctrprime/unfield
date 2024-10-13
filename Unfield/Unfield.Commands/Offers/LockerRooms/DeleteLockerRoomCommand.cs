using Mediator;
using Unfield.DTO.Offers.LockerRooms;

namespace Unfield.Commands.Offers.LockerRooms;

public sealed class DeleteLockerRoomCommand : BaseCommand, IRequest<DeleteLockerRoomDto>
{
    public int LockerRoomId { get; set; }
}