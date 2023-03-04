using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.LockerRooms;
using StadiumEngine.Handlers.Commands.Offers.LockerRooms;
using StadiumEngine.Handlers.Facades.Offers.LockerRooms;

namespace StadiumEngine.Handlers.Handlers.Offers.LockerRooms;

internal sealed class UpdateLockerRoomHandler : BaseCommandHandler<UpdateLockerRoomCommand, UpdateLockerRoomDto>
{
    private readonly IUpdateLockerRoomFacade _facade;

    public UpdateLockerRoomHandler(
        IUpdateLockerRoomFacade facade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _facade = facade;
    }

    protected override async ValueTask<UpdateLockerRoomDto> HandleCommand(UpdateLockerRoomCommand request, CancellationToken cancellationToken)
    {
        return await _facade.Update(request, _currentStadiumId, _userId);
    }
}