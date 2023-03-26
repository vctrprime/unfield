using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.LockerRooms;
using StadiumEngine.Commands.Offers.LockerRooms;

namespace StadiumEngine.Handlers.Handlers.Offers.LockerRooms;

internal sealed class AddLockerRoomHandler : BaseCommandHandler<AddLockerRoomCommand, AddLockerRoomDto>
{
    private readonly ILockerRoomCommandFacade _lockerRoomFacade;

    public AddLockerRoomHandler(
        ILockerRoomCommandFacade lockerRoomFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _lockerRoomFacade = lockerRoomFacade;
    }

    protected override async ValueTask<AddLockerRoomDto> HandleCommandAsync( AddLockerRoomCommand request,
        CancellationToken cancellationToken )
    {
        LockerRoom? lockerRoom = Mapper.Map<LockerRoom>( request );
        lockerRoom.StadiumId = _currentStadiumId;
        lockerRoom.UserCreatedId = _userId;

        _lockerRoomFacade.AddLockerRoom( lockerRoom );

        return await Task.Run( () => new AddLockerRoomDto(), cancellationToken );
    }
}