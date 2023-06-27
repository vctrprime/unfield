using AutoMapper;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Services.Application.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.LockerRooms;
using StadiumEngine.Queries.Offers.LockerRooms;

namespace StadiumEngine.Handlers.Handlers.Offers.LockerRooms;

internal sealed class GetLockerRoomsHandler : BaseRequestHandler<GetLockerRoomsQuery, List<LockerRoomDto>>
{
    private readonly ILockerRoomQueryService _queryService;

    public GetLockerRoomsHandler(
        ILockerRoomQueryService queryService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _queryService = queryService;
    }

    public override async ValueTask<List<LockerRoomDto>> Handle( GetLockerRoomsQuery request,
        CancellationToken cancellationToken )
    {
        List<LockerRoom> lockerRooms = await _queryService.GetByStadiumIdAsync( _currentStadiumId );

        List<LockerRoomDto>? lockerRoomsDto = Mapper.Map<List<LockerRoomDto>>( lockerRooms );

        return lockerRoomsDto;
    }
}