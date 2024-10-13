using AutoMapper;
using Unfield.Commands.EventBus.Bookings;
using Unfield.Domain;
using Unfield.Domain.Entities.Bookings;
using Unfield.Domain.Services.Core.Notifications;
using Unfield.Domain.Services.Core.Schedule;
using Unfield.Services.Notifications.Builders;

namespace Unfield.Handlers.Handlers.EventBus.Bookings;

internal class BookingCanceledCommandHandler : BaseCommandHandler<BookingCanceledCommand, bool>
{
    private readonly ISchedulerBookingQueryService _queryService;
    private readonly IUINotificationService _notificationService;

    public BookingCanceledCommandHandler(
        ISchedulerBookingQueryService queryService,
        IUINotificationService notificationService,
        IMapper mapper, 
        IUnitOfWork unitOfWork ) : base( mapper, null, unitOfWork )
    {
        _queryService = queryService;
        _notificationService = notificationService;
    }

    protected override async ValueTask<bool> HandleCommandAsync(
        BookingCanceledCommand request,
        CancellationToken cancellationToken )
    {
        Booking booking = await _queryService.GetBookingAsync( request.BookingNumber, true );
        await _notificationService.Notify( new CancelBookingUIMessageBuilder( booking, request.Day, request.Reason ) );

        return true;
    }
}