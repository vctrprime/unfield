using AutoMapper;
using Unfield.Commands.Offers.LockerRooms;
using Unfield.Common.Enums.Offers;
using Unfield.Domain;
using Unfield.Domain.Services.Core.Offers;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Offers.LockerRooms;

namespace Unfield.Handlers.Handlers.Offers.LockerRooms;

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