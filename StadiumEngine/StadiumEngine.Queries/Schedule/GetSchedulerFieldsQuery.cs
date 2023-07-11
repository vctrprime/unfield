using Mediator;
using StadiumEngine.DTO.Schedule;

namespace StadiumEngine.Queries.Schedule;

public sealed class GetSchedulerFieldsQuery : BaseQuery, IRequest<SchedulerFieldsDto>
{
}