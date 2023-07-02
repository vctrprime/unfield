using AutoMapper;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Services.Core.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.LockerRooms;
using StadiumEngine.Queries.Offers.LockerRooms;
using StadiumEngine.Services.Resolvers.Offers;

namespace StadiumEngine.Handlers.Handlers.Offers.LockerRooms;

internal sealed class GetLockerRoomsHandler : BaseRequestHandler<GetLockerRoomsQuery, List<LockerRoomDto>>
{
    private readonly ILockerRoomQueryService _queryService;
    private readonly ILockerRoomStatusResolver _lockerRoomStatusResolver;

    public GetLockerRoomsHandler(
        ILockerRoomQueryService queryService,
        ILockerRoomStatusResolver lockerRoomStatusResolver,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _queryService = queryService;
        _lockerRoomStatusResolver = lockerRoomStatusResolver;
    }

    public override async ValueTask<List<LockerRoomDto>> Handle( GetLockerRoomsQuery request,
        CancellationToken cancellationToken )
    {
        List<LockerRoom> lockerRooms = await _queryService.GetByStadiumIdAsync( _currentStadiumId );

        List<LockerRoomDto> lockerRoomsDto = Mapper.Map<List<LockerRoomDto>>( lockerRooms );
        lockerRoomsDto.ForEach(
            l =>
            {
                l.Status = _lockerRoomStatusResolver.Resolve(
                    lockerRooms.First( x => x.Id == l.Id ),
                    request.ClientDate );
            } );

        return lockerRoomsDto;
    }
}