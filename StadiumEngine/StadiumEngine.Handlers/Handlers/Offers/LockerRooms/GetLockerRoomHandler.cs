using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.LockerRooms;
using StadiumEngine.Queries.Offers.LockerRooms;

namespace StadiumEngine.Handlers.Handlers.Offers.LockerRooms;

internal sealed class GetLockerRoomHandler : BaseRequestHandler<GetLockerRoomQuery, LockerRoomDto>
{
    private readonly ILockerRoomQueryFacade _lockerRoomFacade;

    public GetLockerRoomHandler(
        ILockerRoomQueryFacade lockerRoomFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _lockerRoomFacade = lockerRoomFacade;
    }

    public override async ValueTask<LockerRoomDto> Handle( GetLockerRoomQuery request,
        CancellationToken cancellationToken )
    {
        LockerRoom? lockerRoom = await _lockerRoomFacade.GetByLockerRoomIdAsync( request.LockerRoomId, _currentStadiumId );

        if ( lockerRoom == null )
        {
            throw new DomainException( ErrorsKeys.LockerRoomNotFound );
        }

        LockerRoomDto? lockerRoomDto = Mapper.Map<LockerRoomDto>( lockerRoom );

        return lockerRoomDto;
    }
}