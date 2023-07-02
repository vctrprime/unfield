using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Services.Core.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.LockerRooms;
using StadiumEngine.Queries.Offers.LockerRooms;
using StadiumEngine.Services.Resolvers.Offers;

namespace StadiumEngine.Handlers.Handlers.Offers.LockerRooms;

internal sealed class GetLockerRoomHandler : BaseRequestHandler<GetLockerRoomQuery, LockerRoomDto>
{
    private readonly ILockerRoomQueryService _queryService;
    private readonly ILockerRoomStatusResolver _lockerRoomStatusResolver;

    public GetLockerRoomHandler(
        ILockerRoomQueryService queryService,
        ILockerRoomStatusResolver lockerRoomStatusResolver,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _queryService = queryService;
        _lockerRoomStatusResolver = lockerRoomStatusResolver;
    }

    public override async ValueTask<LockerRoomDto> Handle( GetLockerRoomQuery request,
        CancellationToken cancellationToken )
    {
        LockerRoom? lockerRoom = await _queryService.GetByLockerRoomIdAsync( request.LockerRoomId, _currentStadiumId );

        if ( lockerRoom == null )
        {
            throw new DomainException( ErrorsKeys.LockerRoomNotFound );
        }

        LockerRoomDto lockerRoomDto = Mapper.Map<LockerRoomDto>( lockerRoom );
        lockerRoomDto.Status = _lockerRoomStatusResolver.Resolve( lockerRoom, request.ClientDate );

        return lockerRoomDto;
    }
}