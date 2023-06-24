using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Services.Facades.Schedule;
using StadiumEngine.Domain.Services.Models.Schedule;
using StadiumEngine.Services.Facades.Services.Bookings;

namespace StadiumEngine.Services.Facades.Schedule;

internal class SchedulerQueryFacade : ISchedulerQueryFacade
{
    private readonly IBookingFacade _bookingFacade;

    public SchedulerQueryFacade( IBookingFacade bookingFacade )
    {
        _bookingFacade = bookingFacade;
    }

    public async Task<List<SchedulerEvent>> GetEventsAsync( DateTime from, DateTime to, int stadiumId )
    {
        List<Booking> bookings = await _bookingFacade.GetAsync( from, to, stadiumId );

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