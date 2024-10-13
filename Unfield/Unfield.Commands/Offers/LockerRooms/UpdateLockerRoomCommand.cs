using Mediator;
using Unfield.Common.Enums.Offers;
using Unfield.DTO.Offers.LockerRooms;

namespace Unfield.Commands.Offers.LockerRooms;

public sealed class UpdateLockerRoomCommand : BaseCommand, IRequest<UpdateLockerRoomDto>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public LockerRoomGender Gender { get; set; }
    public bool IsActive { get; set; }
}