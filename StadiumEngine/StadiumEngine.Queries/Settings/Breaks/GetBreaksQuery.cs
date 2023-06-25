using Mediator;
using StadiumEngine.DTO.Settings.Breaks;

namespace StadiumEngine.Queries.Settings.Breaks;

public sealed class GetBreaksQuery : IRequest<List<BreakDto>>
{
}