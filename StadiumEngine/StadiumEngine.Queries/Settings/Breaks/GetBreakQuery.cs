using Mediator;
using StadiumEngine.DTO.Settings.Breaks;

namespace StadiumEngine.Queries.Settings.Breaks;

public class GetBreakQuery : BaseQuery, IRequest<BreakDto>
{
    public int BreakId { get; set; }
}