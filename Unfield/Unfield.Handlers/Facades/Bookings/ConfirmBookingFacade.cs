using Unfield.Commands.BookingForm;
using Unfield.Common.Enums.Bookings;
using Unfield.Domain.Entities.Bookings;
using Unfield.Domain.Services.Core.BookingForm;
using Unfield.Domain.Services.Core.BookingForm.Builders;
using Unfield.Domain.Services.Core.Notifications;
using Unfield.Domain.Services.Infrastructure;
using Unfield.DTO.BookingForm;

namespace Unfield.Handlers.Facades.Bookings;

internal class ConfirmBookingFacade : IConfirmBookingFacade
{
    private readonly IBookingCheckoutCommandService _commandService;
    private readonly INotificationsQueueManager _notificationsQueueManager;
    private readonly ICustomerAccountRedirectUrlBuilder _customerAccountRedirectUrlBuilder;

    public ConfirmBookingFacade( 
        IBookingCheckoutCommandService commandService,
        INotificationsQueueManager notificationsQueueManager,
        ICustomerAccountRedirectUrlBuilder customerAccountRedirectUrlBuilder )
    {
        _commandService = commandService;
        _notificationsQueueManager = notificationsQueueManager;
        _customerAccountRedirectUrlBuilder = customerAccountRedirectUrlBuilder;
    }

    public async Task<ConfirmBookingDto> ConfirmAsync( ConfirmBookingCommand request )
    {
        Booking booking = await _commandService.ConfirmBookingAsync( request.BookingNumber, request.AccessCode );
        BookingToken? redirectToken =
            booking.Tokens.SingleOrDefault( x => x.Type == BookingTokenType.RedirectToClientAccountAfterConfirm );
        
        _notificationsQueueManager.EnqueueBookingConfirmedNotification( request.BookingNumber );

        return new ConfirmBookingDto
        {
            RedirectUrl = _customerAccountRedirectUrlBuilder.Build( redirectToken?.Token, request.Language ),
        };
    }
}