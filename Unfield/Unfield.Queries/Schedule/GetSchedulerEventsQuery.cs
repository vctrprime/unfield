using Mediator;
using Unfield.DTO.Schedule;

namespace Unfield.Queries.Schedule;

public sealed class GetSchedulerEventsQuery : BaseQuery, IRequest<List<SchedulerEventDto>>
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string? Language { get; set; }
    public bool? WithDisabledEvents { get; set; }
    public int? StadiumId { get; set; }
    public string? CustomerPhoneNumber { get; set; }
}