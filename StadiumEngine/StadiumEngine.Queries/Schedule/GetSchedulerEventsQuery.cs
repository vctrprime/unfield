using Mediator;
using StadiumEngine.DTO.Schedule;

namespace StadiumEngine.Queries.Schedule;

public sealed class GetSchedulerEventsQuery : BaseQuery, IRequest<List<SchedulerEventDto>>
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string? Language { get; set; }
    public bool? WithDisabledEvents { get; set; }
}