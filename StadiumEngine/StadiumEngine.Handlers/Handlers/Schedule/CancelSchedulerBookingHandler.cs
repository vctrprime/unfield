using AutoMapper;
using StadiumEngine.Commands.Schedule;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Services.Core.Schedule;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Schedule;
using StadiumEngine.Handlers.Facades.Bookings;

namespace StadiumEngine.Handlers.Handlers.Schedule;

internal sealed class CancelSchedulerBookingHandler : BaseCommandHandler<CancelSchedulerBookingCommand, CancelSchedulerBookingDto>
{
    private readonly ICancelBookingFacade _cancelBookingFacade;

    public CancelSchedulerBookingHandler(
        ICancelBookingFacade cancelBookingFacade,
        IClaimsIdentityService claimsIdentityService,
        IMapper mapper,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _cancelBookingFacade = cancelBookingFacade;
    }

    protected override async ValueTask<CancelSchedulerBookingDto> HandleCommandAsync(
        CancelSchedulerBookingCommand request,
        CancellationToken cancellationToken )
    {
        await _cancelBookingFacade.CancelBooking( request, _userId, null );
        return new CancelSchedulerBookingDto();
    }
}