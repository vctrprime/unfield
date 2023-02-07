using AutoMapper;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.LockerRooms;
using StadiumEngine.Handlers.Queries.Offers.LockerRooms;

namespace StadiumEngine.Handlers.Handlers.Offers.LockerRooms;

internal sealed class GetLockerRoomsHandler : BaseRequestHandler<GetLockerRoomsQuery, List<LockerRoomDto>>
{
    private readonly ILockerRoomRepository _repository;

    public GetLockerRoomsHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUnitOfWork unitOfWork, ILockerRoomRepository repository) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _repository = repository;
    }
    
    public override async ValueTask<List<LockerRoomDto>> Handle(GetLockerRoomsQuery request, CancellationToken cancellationToken)
    {
        var lockerRooms = await _repository.GetAll(_currentStadiumId);

        var lockerRoomsDto = Mapper.Map<List<LockerRoomDto>>(lockerRooms);

        return lockerRoomsDto;
    }
}