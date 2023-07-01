using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Services.Core.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.LockerRooms;
using StadiumEngine.Commands.Offers.LockerRooms;

namespace StadiumEngine.Handlers.Handlers.Offers.LockerRooms;

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