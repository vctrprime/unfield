using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.LockerRooms;
using StadiumEngine.Handlers.Commands.Offers.LockerRooms;

namespace StadiumEngine.Handlers.Handlers.Offers.LockerRooms;

internal sealed class DeleteLockerRoomHandler : BaseRequestHandler<DeleteLockerRoomCommand, DeleteLockerRoomDto>
{
    private readonly ILockerRoomRepository _repository;

    public DeleteLockerRoomHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUnitOfWork unitOfWork, ILockerRoomRepository repository) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _repository = repository;
    }

    public override async ValueTask<DeleteLockerRoomDto> Handle(DeleteLockerRoomCommand request, CancellationToken cancellationToken)
    {
        var lockerRoom = await _repository.Get(request.LockerRoomId, _currentStadiumId);

        if (lockerRoom == null) throw new DomainException(ErrorsKeys.LockerRoomNotFound);
        
        _repository.Remove(lockerRoom);
        await UnitOfWork.SaveChanges();

        return new DeleteLockerRoomDto();
    }
}