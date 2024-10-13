using Unfield.Commands;
using Unfield.Common;
using Unfield.Common.Exceptions;
using Unfield.Domain.Entities.Bookings;
using Unfield.Domain.Services.Core.Notifications;
using Unfield.Domain.Services.Core.Schedule;
using Unfield.Domain.Services.Infrastructure;
using Unfield.MessageContracts.Bookings;

namespace Unfield.Handlers.Facades.Bookings;

internal class CancelBookingFacade : ICancelBookingFacade
{
    private readonly ISchedulerBookingQueryService _queryService;
    private readonly ISchedulerBookingCommandService _commandService;
    private readonly INotificationsQueueManager _notificationsQueueManager;
    
    public CancelBookingFacade( ISchedulerBookingQueryService queryService, ISchedulerBookingCommandService commandService, INotificationsQueueManager notificationsQueueManager )
    {
        _queryService = queryService;
        _commandService = commandService;
        _notificationsQueueManager = notificationsQueueManager;
    }
    
    public async Task CancelBooking( BaseCancelBookingCommand request, int? userId, string? customerPhoneNumber )
    {
        BaseCancelBookingCommandBody data = request.Data;
        Booking booking = await _queryService.GetBookingAsync( data.BookingNumber );

        if ( !String.IsNullOrEmpty( customerPhoneNumber ) && customerPhoneNumber != booking.Customer.PhoneNumber )
        {
            throw new DomainException( ErrorsKeys.BookingNotFound );
        }
        
        if ( !booking.IsWeekly )
        {
            if ( booking.Day.AddHours( ( double )booking.StartHour + ( double )booking.HoursCount ) < request.ClientDate )
            {
                throw new DomainException( ErrorsKeys.BookingNotFound );
            }
            
            booking.IsCanceled = true;
            booking.CancelReason = data.Reason;
            booking.UserModifiedId = userId;
            booking.CancelByCustomer = userId is null;
            
            _commandService.UpdateOldVersion( booking );
        }
        else
        {
            if ( data.Day.AddHours( ( double )booking.StartHour + ( double )booking.HoursCount ) < request.ClientDate )
            {
                throw new DomainException( ErrorsKeys.BookingNotFound );
            }
            
            if ( data.CancelOneInRow )
            {
                _commandService.AddExcludeDay( booking.Id, data.Day.Date, userId, data.Reason );
            }
            else
            {
                booking.IsWeeklyStoppedDate = request.ClientDate;
                booking.CancelReason = data.Reason;
                booking.UserModifiedId = userId;
                booking.CancelByCustomer = userId is null;
                
                _commandService.UpdateOldVersion( booking );
            }
        }

        // только если отменил заказчик
        if ( !userId.HasValue )
        {
            _notificationsQueueManager.EnqueueBookingCanceledNotification( data.BookingNumber, data.Day, data.Reason );
        }
    }
}