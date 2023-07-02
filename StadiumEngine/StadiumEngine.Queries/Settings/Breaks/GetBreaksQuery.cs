using Mediator;
using StadiumEngine.DTO.Settings.Breaks;

namespace StadiumEngine.Queries.Settings.Breaks;

public sealed class GetBreaksQuery : BaseQuery, IRequest<List<BreakDto>>
{
}