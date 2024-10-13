using Unfield.Common;
using Unfield.Common.Exceptions;
using Unfield.Domain.Entities.Offers;
using Unfield.Domain.Services.Core.Offers;
using Unfield.DTO.Offers.LockerRooms;
using Unfield.Commands.Offers.LockerRooms;

namespace Unfield.Handlers.Facades.Offers.LockerRooms;

internal class UpdateLockerRoomFacade : IUpdateLockerRoomFacade
{
    private readonly ILockerRoomCommandService _commandService;
    private readonly ILockerRoomQueryService _queryService;

    public UpdateLockerRoomFacade( ILockerRoomQueryService queryService, ILockerRoomCommandService commandService )
    {
        _queryService = queryService;
        _commandService = commandService;
    }

    public async Task<UpdateLockerRoomDto> UpdateAsync( UpdateLockerRoomCommand request, int stadiumId, int userId )
    {
        LockerRoom? lockerRoom = await _queryService.GetByLockerRoomIdAsync( request.Id, stadiumId );

        if ( lockerRoom == null )
        {
            throw new DomainException( ErrorsKeys.LockerRoomNotFound );
        }

        lockerRoom.Name = request.Name;
        lockerRoom.Description = request.Description;
        lockerRoom.Gender = request.Gender;
        lockerRoom.IsActive = request.IsActive;
        lockerRoom.UserModifiedId = userId;

        _commandService.UpdateLockerRoom( lockerRoom );

        return new UpdateLockerRoomDto();
    }
}