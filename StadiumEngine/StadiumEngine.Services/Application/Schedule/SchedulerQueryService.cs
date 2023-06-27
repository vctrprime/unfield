using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Repositories.Settings;
using StadiumEngine.Domain.Services.Application.Schedule;
using StadiumEngine.Domain.Services.Models.Schedule;
using StadiumEngine.Services.Facades.Bookings;

namespace StadiumEngine.Services.Application.Schedule;

internal class SchedulerQueryService : ISchedulerQueryService
{
    private readonly IBookingFacade _bookingFacade;
    private readonly IBreakRepository _breakRepository;

    public SchedulerQueryService( IBookingFacade bookingFacade, IBreakRepository breakRepository )
    {
        _bookingFacade = bookingFacade;
        _breakRepository = breakRepository;
    }

    public async Task<List<SchedulerEvent>> GetEventsAsync( DateTime from, DateTime to, int stadiumId, string language )
    {
        List<Booking> bookings = await _bookingFacade.GetAsync( from, to, stadiumId );
        List<Break> breaks = ( await _breakRepository.GetAllAsync( stadiumId ) ).Where( x => x.IsActive ).ToList();

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

        foreach ( Break @break in breaks )
        {
            DateTime date = from.Date;

            while ( date <= to.Date )
            {
                if ( @break.DateStart <= date && ( !@break.DateEnd.HasValue || @break.DateEnd.Value >= date.Date ) )
                {
                    TimeSpan diff = date - @break.DateStart;
                    events.AddRange(
                        @break.BreakFields.Select( x => new SchedulerEvent( x, @break.DateStart.Add( diff ), language ) ) );
                }

                date = date.AddDays( 1 );
            }
        }

        if ( disabledEvents.Any() )
        {
            events.AddRange( disabledEvents );
        }

        return events;
    }
}