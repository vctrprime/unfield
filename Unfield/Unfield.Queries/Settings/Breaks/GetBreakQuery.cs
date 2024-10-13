using Mediator;
using Unfield.DTO.Settings.Breaks;

namespace Unfield.Queries.Settings.Breaks;

public class GetBreakQuery : BaseQuery, IRequest<BreakDto>
{
    public int BreakId { get; set; }
}