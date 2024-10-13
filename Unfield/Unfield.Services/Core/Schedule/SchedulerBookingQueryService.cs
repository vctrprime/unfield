using Unfield.Common;
using Unfield.Common.Exceptions;
using Unfield.Domain.Entities.Bookings;
using Unfield.Domain.Repositories.Bookings;
using Unfield.Domain.Services.Core.Schedule;

namespace Unfield.Services.Core.Schedule;

internal class SchedulerBookingQueryService : ISchedulerBookingQueryService
{
    private readonly IBookingRepository _bookingRepository;

    public SchedulerBookingQueryService( IBookingRepository bookingRepository )
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<Booking> GetBookingAsync( string bookingNumber, bool includeCancelled = false )
    {
        Booking? booking = await _bookingRepository.GetByNumberAsync( bookingNumber );
        if ( booking == null || ( !includeCancelled && booking.IsCanceled) )
        {
            throw new DomainException( ErrorsKeys.BookingNotFound );
        }

        return booking;
    }
}