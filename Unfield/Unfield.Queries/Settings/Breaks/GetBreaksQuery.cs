using Mediator;
using Unfield.DTO.Settings.Breaks;

namespace Unfield.Queries.Settings.Breaks;

public sealed class GetBreaksQuery : BaseQuery, IRequest<List<BreakDto>>
{
}