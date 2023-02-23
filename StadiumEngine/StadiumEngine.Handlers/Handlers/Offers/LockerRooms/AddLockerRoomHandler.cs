using AutoMapper;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.LockerRooms;
using StadiumEngine.Handlers.Commands.Offers.LockerRooms;

namespace StadiumEngine.Handlers.Handlers.Offers.LockerRooms;

internal sealed class AddLockerRoomHandler : BaseRequestHandler<AddLockerRoomCommand, AddLockerRoomDto>
{
    private readonly ILockerRoomFacade _lockerRoomFacade;

    public AddLockerRoomHandler(
        ILockerRoomFacade lockerRoomFacade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _lockerRoomFacade = lockerRoomFacade;
    }
    
    public override async ValueTask<AddLockerRoomDto> Handle(AddLockerRoomCommand request, CancellationToken cancellationToken)
    {
        var lockerRoom = Mapper.Map<LockerRoom>(request);
        lockerRoom.StadiumId = _currentStadiumId;
        lockerRoom.UserCreatedId = _userId;
        
        _lockerRoomFacade.AddLockerRoom(lockerRoom);
        await UnitOfWork.SaveChanges();

        return new AddLockerRoomDto();
    }
}