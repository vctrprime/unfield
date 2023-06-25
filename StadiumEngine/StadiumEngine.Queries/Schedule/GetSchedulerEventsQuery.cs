using Mediator;
using StadiumEngine.DTO.Schedule;

namespace StadiumEngine.Queries.Schedule;

public sealed class GetSchedulerEventsQuery : IRequest<List<SchedulerEventDto>>
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string? Language { get; set; }
}