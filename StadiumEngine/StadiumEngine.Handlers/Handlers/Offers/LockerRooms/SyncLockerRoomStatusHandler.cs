using AutoMapper;
using StadiumEngine.Commands.Offers.LockerRooms;
using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Core.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.LockerRooms;

namespace StadiumEngine.Handlers.Handlers.Offers.LockerRooms;

internal sealed class
    SyncLockerRoomStatusHandler : BaseCommandHandler<SyncLockerRoomStatusCommand, SyncLockerRoomStatusDto>
{
    private readonly ILockerRoomCommandService _commandService;

    public SyncLockerRoomStatusHandler(
        ILockerRoomCommandService commandService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base(
        mapper,
        claimsIdentityService,
        unitOfWork)
    {
        _commandService = commandService;
    }

    protected override async ValueTask<SyncLockerRoomStatusDto> HandleCommandAsync(
        SyncLockerRoomStatusCommand request,
        CancellationToken cancellationToken )
    {
        LockerRoomStatus status = await _commandService.SyncStatusAsync( request.LockerRoomId, _currentStadiumId, request.ClientDate );

        return new SyncLockerRoomStatusDto
        {
            Status = status
        };
    }
}