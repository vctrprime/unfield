using StadiumEngine.Domain.Services.Models.Schedule;

namespace StadiumEngine.Domain.Services.Facades.Schedule;

public interface ISchedulerQueryFacade
{
    Task<List<SchedulerEvent>> GetEventsAsync( DateTime from, DateTime to, int stadiumId, string language );
}