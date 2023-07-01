using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Services.Core.Offers;
using StadiumEngine.DTO.Offers.LockerRooms;
using StadiumEngine.Commands.Offers.LockerRooms;

namespace StadiumEngine.Handlers.Facades.Offers.LockerRooms;

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