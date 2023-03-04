using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.LockerRooms;
using StadiumEngine.Handlers.Commands.Offers.LockerRooms;

namespace StadiumEngine.Handlers.Handlers.Offers.LockerRooms;

internal sealed class DeleteLockerRoomHandler : BaseCommandHandler<DeleteLockerRoomCommand, DeleteLockerRoomDto>
{
    private readonly ILockerRoomFacade _lockerRoomFacade;

    public DeleteLockerRoomHandler(
        ILockerRoomFacade lockerRoomFacade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _lockerRoomFacade = lockerRoomFacade;
    }

    protected override async ValueTask<DeleteLockerRoomDto> HandleCommand(DeleteLockerRoomCommand request, CancellationToken cancellationToken)
    {
        await _lockerRoomFacade.DeleteLockerRoom(request.LockerRoomId, _currentStadiumId);
        return new DeleteLockerRoomDto();
    }
}