using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.LockerRooms;
using StadiumEngine.Handlers.Commands.Offers.LockerRooms;

namespace StadiumEngine.Handlers.Handlers.Offers.LockerRooms;

internal sealed class DeleteLockerRoomHandler : BaseRequestHandler<DeleteLockerRoomCommand, DeleteLockerRoomDto>
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

    public override async ValueTask<DeleteLockerRoomDto> Handle(DeleteLockerRoomCommand request, CancellationToken cancellationToken)
    {
        await _lockerRoomFacade.DeleteLockerRoom(request.LockerRoomId, _currentStadiumId);
        await UnitOfWork.SaveChanges();

        return new DeleteLockerRoomDto();
    }
}