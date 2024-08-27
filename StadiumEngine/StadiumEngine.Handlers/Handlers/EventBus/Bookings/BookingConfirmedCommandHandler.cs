using AutoMapper;
using StadiumEngine.Commands.EventBus.Bookings;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Services.Core.BookingForm;
using StadiumEngine.Domain.Services.Core.BookingForm.Distributors;
using StadiumEngine.Domain.Services.Core.Notifications;
using StadiumEngine.Services.Notifications.Builders;

namespace StadiumEngine.Handlers.Handlers.EventBus.Bookings;

internal class BookingConfirmedCommandHandler : BaseCommandHandler<BookingConfirmedCommand, bool>
{
    private readonly IBookingCheckoutQueryService _bookingCheckoutQueryService;
    private readonly IUINotificationService _notificationService;
    private readonly IBookingLockerRoomDistributor _lockerRoomDistributor;

    public BookingConfirmedCommandHandler(
        IBookingCheckoutQueryService bookingCheckoutQueryService,
        IUINotificationService notificationService,
        IBookingLockerRoomDistributor lockerRoomDistributor,
        IMapper mapper, 
        IUnitOfWork unitOfWork ) : base( mapper, null, unitOfWork )
    {
        _bookingCheckoutQueryService = bookingCheckoutQueryService;
        _notificationService = notificationService;
        _lockerRoomDistributor = lockerRoomDistributor;
    }

    protected override async ValueTask<bool> HandleCommandAsync(
        BookingConfirmedCommand request,
        CancellationToken cancellationToken )
    {
        Booking booking = await _bookingCheckoutQueryService.GetConfirmedBookingAsync( request.BookingNumber );

        await DistributeLockerRoom( booking );
        
        await Notify( booking );
        
        return true;
    }
    
    private async Task Notify( Booking booking ) => 
        await _notificationService.Notify( new NewBookingUIMessageBuilder( booking ) );
    
    private async Task DistributeLockerRoom( Booking booking ) =>
        await _lockerRoomDistributor.DistributeAsync( booking );
}