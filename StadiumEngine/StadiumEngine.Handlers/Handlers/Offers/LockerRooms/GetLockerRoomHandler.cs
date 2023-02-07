using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.LockerRooms;
using StadiumEngine.Handlers.Queries.Offers.LockerRooms;

namespace StadiumEngine.Handlers.Handlers.Offers.LockerRooms;

internal sealed class GetLockerRoomHandler : BaseRequestHandler<GetLockerRoomQuery, LockerRoomDto>
{
    private readonly ILockerRoomRepository _repository;

    public GetLockerRoomHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUnitOfWork unitOfWork, ILockerRoomRepository repository) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _repository = repository;
    }
    
    public override async ValueTask<LockerRoomDto> Handle(GetLockerRoomQuery request, CancellationToken cancellationToken)
    {
        var lockerRoom = await _repository.Get(request.LockerRoomId, _currentStadiumId);

        if (lockerRoom == null) throw new DomainException(ErrorsKeys.LockerRoomNotFound);

        var lockerRoomDto = Mapper.Map<LockerRoomDto>(lockerRoom);

        return lockerRoomDto;
    }
}