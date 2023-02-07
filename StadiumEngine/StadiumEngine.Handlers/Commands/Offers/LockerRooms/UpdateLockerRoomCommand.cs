using Mediator;
using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.DTO.Offers.LockerRooms;

namespace StadiumEngine.Handlers.Commands.Offers.LockerRooms;

public sealed class UpdateLockerRoomCommand : IRequest<UpdateLockerRoomDto>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public LockerRoomGender Gender { get; set; }
    public bool IsActive { get; set; }
}