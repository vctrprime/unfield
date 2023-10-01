using AutoMapper;
using StadiumEngine.Commands.Schedule;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Services.Core.Schedule;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Schedule;

namespace StadiumEngine.Handlers.Handlers.Schedule;

internal sealed class CancelSchedulerBookingHandler : BaseCommandHandler<CancelSchedulerBookingCommand, CancelSchedulerBookingDto>
{
    private readonly ISchedulerBookingQueryService _queryService;
    private readonly ISchedulerBookingCommandService _commandService;

    public CancelSchedulerBookingHandler(
        ISchedulerBookingQueryService queryService,
        ISchedulerBookingCommandService commandService,
        IClaimsIdentityService claimsIdentityService,
        IMapper mapper,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _queryService = queryService;
        _commandService = commandService;
    }

    protected override async ValueTask<CancelSchedulerBookingDto> HandleCommandAsync(
        CancelSchedulerBookingCommand request,
        CancellationToken cancellationToken )
    {
        Booking booking = await _queryService.GetBookingAsync( request.BookingNumber );
        if ( !booking.IsWeekly )
        {
            booking.IsCanceled = true;
            booking.UserModifiedId = _userId;
            booking.CancelReason = request.Reason;
            _commandService.UpdateOldVersion( booking );
        }
        else
        {
            if ( request.CancelOneInRow )
            {
                _commandService.AddExcludeDay( booking.Id, request.Day, _userId, request.Reason );
            }
            else
            {
                booking.IsWeeklyStoppedDate = request.ClientDate;
                booking.UserModifiedId = _userId;
                booking.CancelReason = request.Reason;
                
                _commandService.UpdateOldVersion( booking );
            }
        }

        return new CancelSchedulerBookingDto();
    }
}