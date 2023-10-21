using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Repositories.Settings;
using StadiumEngine.Domain.Services.Core.Schedule;
using StadiumEngine.Domain.Services.Models.Schedule;
using StadiumEngine.Services.Facades.Bookings;

namespace StadiumEngine.Services.Core.Schedule;

internal class SchedulerQueryService : ISchedulerQueryService
{
    private readonly IBookingFacade _bookingFacade;
    private readonly IBreakRepository _breakRepository;

    public SchedulerQueryService( IBookingFacade bookingFacade, IBreakRepository breakRepository )
    {
        _bookingFacade = bookingFacade;
        _breakRepository = breakRepository;
    }

    public async Task<List<SchedulerEvent>> GetEventsAsync(
        DateTime from,
        DateTime to,
        int stadiumId,
        string language, 
        bool withDisabledEvents )
    {
        List<Booking> bookings = await _bookingFacade.GetAsync( from, to, stadiumId );
        List<SchedulerEvent> events = bookings.Where( x => x.IsConfirmed && !x.IsCanceled )
            .Select( x => new SchedulerEvent( x, language ) ).ToList();

        if ( withDisabledEvents )
        {
            List<SchedulerEvent> disabledEvents = await BuildDisabledEvents(
                stadiumId,
                bookings,
                from,
                to,
                language );

            if ( disabledEvents.Any() )
            {
                events.AddRange( disabledEvents );
            }
        }
        
        return events;
    }

    private async Task<List<SchedulerEvent>> BuildDisabledEvents(
        int stadiumId,
        List<Booking> bookings,
        DateTime from,
        DateTime to,
        string language )
    {
        List<SchedulerEvent> result = new List<SchedulerEvent>();

        List<SchedulerEvent> breaks = await BuildBreaks(
            stadiumId,
            from,
            to,
            language );
        if ( breaks.Any() )
        {
            result.AddRange( breaks );
        }

        BuildChildFieldsBookings( bookings, result, language );

        return result;
    }

    private async Task<List<SchedulerEvent>> BuildBreaks(
        int stadiumId,
        DateTime from,
        DateTime to,
        string language )
    {
        List<SchedulerEvent> result = new List<SchedulerEvent>();

        List<Break> breaks = ( await _breakRepository.GetAllAsync( stadiumId ) ).Where( x => x.IsActive ).ToList();

        foreach ( Break @break in breaks )
        {
            DateTime date = from.Date;

            while ( date <= to.Date )
            {
                if ( @break.DateStart <= date && ( !@break.DateEnd.HasValue || @break.DateEnd.Value >= date.Date ) )
                {
                    TimeSpan diff = date - @break.DateStart;
                    result.AddRange(
                        @break.BreakFields.Select(
                            x => new SchedulerEvent( x, @break.DateStart.Add( diff ), language ) ) );
                }

                date = date.AddDays( 1 );
            }
        }

        return result;
    }

    private void BuildChildFieldsBookings(
        List<Booking> bookings,
        List<SchedulerEvent> disabledEvents,
        string language )
    {
        foreach ( Booking booking in bookings.OrderBy( x => x.StartHour ) )
        {
            if ( booking.Field.ParentFieldId.HasValue )
            {
                ( DateTime? startDate, DateTime? endDate ) = BreakChildFieldIntersection(
                    booking,
                    disabledEvents,
                    booking.Field.ParentFieldId.Value );
                if ( !startDate.HasValue || !endDate.HasValue )
                {
                    continue;
                }

                SchedulerEvent? disabledEvent = disabledEvents.FirstOrDefault(
                    x =>
                        x.EventId != 0
                        && x.End >= startDate
                        && x.Start <= startDate
                        && x.FieldId == booking.Field.ParentFieldId );

                if ( disabledEvent == null )
                {
                    disabledEvents.Add(
                        new SchedulerEvent(
                            booking,
                            language,
                            booking.Field.ParentFieldId.Value,
                            startDate,
                            endDate ) );
                }
                else
                {
                    disabledEvent.End = endDate.Value > disabledEvent.End ? endDate.Value : disabledEvent.End;
                    disabledEvent.BookingsCount++;
                }
            }

            foreach ( Field childField in booking.Field.ChildFields )
            {
                ( DateTime? startDate, DateTime? endDate ) =
                    BreakChildFieldIntersection( booking, disabledEvents, childField.Id );
                if ( !startDate.HasValue || !endDate.HasValue )
                {
                    continue;
                }

                disabledEvents.Add(
                    new SchedulerEvent(
                        booking,
                        language,
                        childField.Id,
                        startDate,
                        endDate ) );
            }
        }
    }

    private (DateTime?, DateTime?) BreakChildFieldIntersection(
        Booking booking,
        List<SchedulerEvent> disabledEvents,
        int fieldId )
    {
        DateTime startDate = booking.Day.AddHours( ( double )booking.StartHour );
        DateTime endDate = booking.Day.AddHours( ( double )booking.StartHour + ( double )booking.HoursCount );

        SchedulerEvent? @break = disabledEvents.SingleOrDefault(
            x => x.EventId == 0
                 && x.FieldId == fieldId
                 && (
                     ( x.Start <= startDate && x.End >= startDate )
                     ||
                     ( x.Start <= endDate && x.End >= endDate ) ) );

        if ( @break == null )
        {
            return ( startDate, endDate );
        }

        if ( ( startDate == @break.Start && endDate == @break.End ) ||
             ( startDate >= @break.Start && endDate <= @break.End ) )
        {
            return ( null, null );
        }

        if ( startDate < @break.Start )
        {
            endDate = @break.Start;
        }

        if ( endDate > @break.End )
        {
            startDate = @break.End;
        }

        return ( startDate, endDate );
    }
}