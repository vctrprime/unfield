using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.LockerRooms;
using StadiumEngine.Handlers.Commands.Offers.LockerRooms;

namespace StadiumEngine.Handlers.Handlers.Offers.LockerRooms;

internal sealed class UpdateLockerRoomHandler : BaseRequestHandler<UpdateLockerRoomCommand, UpdateLockerRoomDto>
{
    private readonly ILockerRoomRepository _repository;

    public UpdateLockerRoomHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUnitOfWork unitOfWork, ILockerRoomRepository repository) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _repository = repository;
    }

    public override async ValueTask<UpdateLockerRoomDto> Handle(UpdateLockerRoomCommand request, CancellationToken cancellationToken)
    {
        var lockerRoom = await _repository.Get(request.Id, _currentStadiumId);

        if (lockerRoom == null) throw new DomainException(ErrorsKeys.LockerRoomNotFound);
        
        lockerRoom.Name = request.Name;
        lockerRoom.Description = request.Description;
        lockerRoom.Gender = request.Gender;
        lockerRoom.IsActive = request.IsActive;
        lockerRoom.UserModifiedId = _userId;
        
        _repository.Update(lockerRoom);
        await UnitOfWork.SaveChanges();

        return new UpdateLockerRoomDto();
    }
}