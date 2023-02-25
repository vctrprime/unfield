using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.LockerRooms;
using StadiumEngine.Handlers.Queries.Offers.LockerRooms;

namespace StadiumEngine.Handlers.Handlers.Offers.LockerRooms;

internal sealed class GetLockerRoomsHandler : BaseRequestHandler<GetLockerRoomsQuery, List<LockerRoomDto>>
{
    private readonly ILockerRoomFacade _lockerRoomFacade;

    public GetLockerRoomsHandler(
        ILockerRoomFacade lockerRoomFacade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _lockerRoomFacade = lockerRoomFacade;
    }
    
    public override async ValueTask<List<LockerRoomDto>> Handle(GetLockerRoomsQuery request, CancellationToken cancellationToken)
    {
        var lockerRooms = await _lockerRoomFacade.GetByStadiumId(_currentStadiumId);

        var lockerRoomsDto = Mapper.Map<List<LockerRoomDto>>(lockerRooms);

        return lockerRoomsDto;
    }
}