using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Core.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.LockerRooms;
using StadiumEngine.Commands.Offers.LockerRooms;

namespace StadiumEngine.Handlers.Handlers.Offers.LockerRooms;

internal sealed class DeleteLockerRoomHandler : BaseCommandHandler<DeleteLockerRoomCommand, DeleteLockerRoomDto>
{
    private readonly ILockerRoomCommandService _commandService;

    public DeleteLockerRoomHandler(
        ILockerRoomCommandService commandService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _commandService = commandService;
    }

    protected override async ValueTask<DeleteLockerRoomDto> HandleCommandAsync( DeleteLockerRoomCommand request,
        CancellationToken cancellationToken )
    {
        await _commandService.DeleteLockerRoomAsync( request.LockerRoomId, _currentStadiumId );
        return new DeleteLockerRoomDto();
    }
}