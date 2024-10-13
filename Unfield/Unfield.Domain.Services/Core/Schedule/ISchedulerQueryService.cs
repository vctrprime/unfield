using Unfield.Domain.Services.Models.Schedule;

namespace Unfield.Domain.Services.Core.Schedule;

public interface ISchedulerQueryService
{
    Task<List<SchedulerEvent>> GetEventsAsync(
        DateTime from,
        DateTime to,
        int stadiumId,
        string language,
        bool withDisabledEvents,
        string? customerPhoneNumber );

    Task<List<BookingListItem>> SearchAllByNumberAsync( string bookingNumber, int stadiumId );
}