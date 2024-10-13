using AutoMapper;
using Unfield.Domain;
using Unfield.Domain.Entities.Offers;
using Unfield.Domain.Services.Core.Offers;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Offers.LockerRooms;
using Unfield.Commands.Offers.LockerRooms;

namespace Unfield.Handlers.Handlers.Offers.LockerRooms;

internal sealed class AddLockerRoomHandler : BaseCommandHandler<AddLockerRoomCommand, AddLockerRoomDto>
{
    private readonly ILockerRoomCommandService _commandService;

    public AddLockerRoomHandler(
        ILockerRoomCommandService commandService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _commandService = commandService;
    }

    protected override async ValueTask<AddLockerRoomDto> HandleCommandAsync( AddLockerRoomCommand request,
        CancellationToken cancellationToken )
    {
        LockerRoom? lockerRoom = Mapper.Map<LockerRoom>( request );
        lockerRoom.StadiumId = _currentStadiumId;
        lockerRoom.UserCreatedId = _userId;

        _commandService.AddLockerRoom( lockerRoom );

        return await Task.Run( () => new AddLockerRoomDto(), cancellationToken );
    }
}