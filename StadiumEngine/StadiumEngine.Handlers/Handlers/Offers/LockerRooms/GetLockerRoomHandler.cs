using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.LockerRooms;
using StadiumEngine.Handlers.Queries.Offers.LockerRooms;

namespace StadiumEngine.Handlers.Handlers.Offers.LockerRooms;

internal sealed class GetLockerRoomHandler : BaseRequestHandler<GetLockerRoomQuery, LockerRoomDto>
{
    private readonly ILockerRoomFacade _lockerRoomFacade;

    public GetLockerRoomHandler(
        ILockerRoomFacade lockerRoomFacade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService) : base(mapper, claimsIdentityService)
    {
        _lockerRoomFacade = lockerRoomFacade;
    }
    
    public override async ValueTask<LockerRoomDto> Handle(GetLockerRoomQuery request, CancellationToken cancellationToken)
    {
        var lockerRoom = await _lockerRoomFacade.GetByLockerRoomId(request.LockerRoomId, _currentStadiumId);

        if (lockerRoom == null) throw new DomainException(ErrorsKeys.LockerRoomNotFound);

        var lockerRoomDto = Mapper.Map<LockerRoomDto>(lockerRoom);

        return lockerRoomDto;
    }
}