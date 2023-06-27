using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Services.Application.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.LockerRooms;
using StadiumEngine.Queries.Offers.LockerRooms;

namespace StadiumEngine.Handlers.Handlers.Offers.LockerRooms;

internal sealed class GetLockerRoomHandler : BaseRequestHandler<GetLockerRoomQuery, LockerRoomDto>
{
    private readonly ILockerRoomQueryService _queryService;

    public GetLockerRoomHandler(
        ILockerRoomQueryService queryService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _queryService = queryService;
    }

    public override async ValueTask<LockerRoomDto> Handle( GetLockerRoomQuery request,
        CancellationToken cancellationToken )
    {
        LockerRoom? lockerRoom = await _queryService.GetByLockerRoomIdAsync( request.LockerRoomId, _currentStadiumId );

        if ( lockerRoom == null )
        {
            throw new DomainException( ErrorsKeys.LockerRoomNotFound );
        }

        LockerRoomDto? lockerRoomDto = Mapper.Map<LockerRoomDto>( lockerRoom );

        return lockerRoomDto;
    }
}