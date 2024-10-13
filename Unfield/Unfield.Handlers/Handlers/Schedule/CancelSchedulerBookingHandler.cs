using AutoMapper;
using Unfield.Commands.Schedule;
using Unfield.Domain;
using Unfield.Domain.Entities.Bookings;
using Unfield.Domain.Services.Core.Schedule;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Schedule;
using Unfield.Handlers.Facades.Bookings;

namespace Unfield.Handlers.Handlers.Schedule;

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