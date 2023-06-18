using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Repositories.Bookings;
using StadiumEngine.Domain.Services.Facades.Schedule;
using StadiumEngine.Domain.Services.Models.Schedule;

namespace StadiumEngine.Services.Facades.Schedule;

internal class SchedulerQueryFacade : ISchedulerQueryFacade
{
    private readonly IBookingRepository _bookingRepository;

    public SchedulerQueryFacade( IBookingRepository bookingRepository )
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<List<SchedulerEvent>> GetEventsAsync( DateTime from, DateTime to, int stadiumId )
    {
        List<Booking> bookings = ( await _bookingRepository.GetAsync( from, to, stadiumId ) )
            .Where( x => x.IsConfirmed && !x.IsCanceled ).ToList();

        List<SchedulerEvent> events = bookings.Where( x => x.IsConfirmed && !x.IsCanceled )
            .Select( x => new SchedulerEvent( x ) ).ToList();

        List<SchedulerEvent> disabledEvents = new List<SchedulerEvent>();
        foreach ( Booking booking in bookings )
        {
            if ( booking.Field.ParentFieldId.HasValue )
            {
                disabledEvents.Add( new SchedulerEvent( booking, booking.Field.ParentFieldId.Value ) );
            }

            if ( booking.Field.ChildFields.Any() )
            {
                disabledEvents.AddRange( booking.Field.ChildFields.Select( x => new SchedulerEvent( booking, x.Id ) ) );
            }
        }

        if ( disabledEvents.Any() )
        {
            events.AddRange( disabledEvents );
        }

        return events;
    }
}