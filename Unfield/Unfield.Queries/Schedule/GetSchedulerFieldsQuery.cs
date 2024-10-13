using Mediator;
using Unfield.DTO.Schedule;

namespace Unfield.Queries.Schedule;

public sealed class GetSchedulerFieldsQuery : BaseQuery, IRequest<SchedulerFieldsDto>
{
}