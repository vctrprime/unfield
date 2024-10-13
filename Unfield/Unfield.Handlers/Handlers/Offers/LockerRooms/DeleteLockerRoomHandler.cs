using AutoMapper;
using Unfield.Domain;
using Unfield.Domain.Services.Core.Offers;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Offers.LockerRooms;
using Unfield.Commands.Offers.LockerRooms;

namespace Unfield.Handlers.Handlers.Offers.LockerRooms;

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