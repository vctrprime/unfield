using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Repositories.Bookings;
using StadiumEngine.Domain.Services.Core.Schedule;

namespace StadiumEngine.Services.Core.Schedule;

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