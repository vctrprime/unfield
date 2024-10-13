using AutoMapper;
using Unfield.Common;
using Unfield.Common.Exceptions;
using Unfield.Domain.Entities.Offers;
using Unfield.Domain.Services.Core.Offers;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Offers.LockerRooms;
using Unfield.Queries.Offers.LockerRooms;
using Unfield.Services.Resolvers.Offers;

namespace Unfield.Handlers.Handlers.Offers.LockerRooms;

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