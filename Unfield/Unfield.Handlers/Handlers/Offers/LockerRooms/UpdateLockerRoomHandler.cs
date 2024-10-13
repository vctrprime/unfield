using AutoMapper;
using Unfield.Domain;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Offers.LockerRooms;
using Unfield.Commands.Offers.LockerRooms;
using Unfield.Handlers.Facades.Offers.LockerRooms;

namespace Unfield.Handlers.Handlers.Offers.LockerRooms;

internal sealed class UpdateLockerRoomHandler : BaseCommandHandler<UpdateLockerRoomCommand, UpdateLockerRoomDto>
{
    private readonly IUpdateLockerRoomFacade _facade;

    public UpdateLockerRoomHandler(
        IUpdateLockerRoomFacade facade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _facade = facade;
    }

    protected override async ValueTask<UpdateLockerRoomDto> HandleCommandAsync( UpdateLockerRoomCommand request,
        CancellationToken cancellationToken ) =>
        await _facade.UpdateAsync( request, _currentStadiumId, _userId );
}