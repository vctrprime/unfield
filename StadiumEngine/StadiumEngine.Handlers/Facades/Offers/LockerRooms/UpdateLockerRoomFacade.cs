using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.DTO.Offers.LockerRooms;
using StadiumEngine.Handlers.Commands.Offers.LockerRooms;

namespace StadiumEngine.Handlers.Facades.Offers.LockerRooms;

internal class UpdateLockerRoomFacade : IUpdateLockerRoomFacade
{
    private readonly ILockerRoomQueryFacade _queryFacade;
    private readonly ILockerRoomCommandFacade _commandFacade;

    public UpdateLockerRoomFacade( ILockerRoomQueryFacade queryFacade, ILockerRoomCommandFacade commandFacade )
    {
        _queryFacade = queryFacade;
        _commandFacade = commandFacade;
    }

    public async Task<UpdateLockerRoomDto> Update( UpdateLockerRoomCommand request, int stadiumId, int userId )
    {
        var lockerRoom = await _queryFacade.GetByLockerRoomId( request.Id, stadiumId );

        if (lockerRoom == null) throw new DomainException( ErrorsKeys.LockerRoomNotFound );

        lockerRoom.Name = request.Name;
        lockerRoom.Description = request.Description;
        lockerRoom.Gender = request.Gender;
        lockerRoom.IsActive = request.IsActive;
        lockerRoom.UserModifiedId = userId;

        _commandFacade.UpdateLockerRoom( lockerRoom );

        return new UpdateLockerRoomDto();
    }
}