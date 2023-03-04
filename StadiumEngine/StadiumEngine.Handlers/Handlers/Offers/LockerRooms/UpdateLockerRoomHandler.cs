using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.LockerRooms;
using StadiumEngine.Handlers.Commands.Offers.LockerRooms;

namespace StadiumEngine.Handlers.Handlers.Offers.LockerRooms;

internal sealed class UpdateLockerRoomHandler : BaseCommandHandler<UpdateLockerRoomCommand, UpdateLockerRoomDto>
{
    private readonly ILockerRoomFacade _lockerRoomFacade;

    public UpdateLockerRoomHandler(
        ILockerRoomFacade lockerRoomFacade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _lockerRoomFacade = lockerRoomFacade;
    }

    protected override async ValueTask<UpdateLockerRoomDto> HandleCommand(UpdateLockerRoomCommand request, CancellationToken cancellationToken)
    {
        var lockerRoom = await _lockerRoomFacade.GetByLockerRoomId(request.Id, _currentStadiumId);

        if (lockerRoom == null) throw new DomainException(ErrorsKeys.LockerRoomNotFound);
        
        lockerRoom.Name = request.Name;
        lockerRoom.Description = request.Description;
        lockerRoom.Gender = request.Gender;
        lockerRoom.IsActive = request.IsActive;
        lockerRoom.UserModifiedId = _userId;
        
        _lockerRoomFacade.UpdateLockerRoom(lockerRoom);
        
        return new UpdateLockerRoomDto();
    }
}