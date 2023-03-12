using Mediator;
using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.DTO.Offers.LockerRooms;

namespace StadiumEngine.Commands.Offers.LockerRooms;

public sealed class AddLockerRoomCommand : IRequest<AddLockerRoomDto>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public LockerRoomGender Gender { get; set; }
    public bool IsActive { get; set; }
}