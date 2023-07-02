using Mediator;
using StadiumEngine.DTO.Offers.LockerRooms;

namespace StadiumEngine.Commands.Offers.LockerRooms;

public sealed class DeleteLockerRoomCommand : BaseCommand, IRequest<DeleteLockerRoomDto>
{
    public int LockerRoomId { get; set; }
}