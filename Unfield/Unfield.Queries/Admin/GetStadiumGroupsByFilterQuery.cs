using Mediator;
using Unfield.DTO.Admin;

namespace Unfield.Queries.Admin;

public sealed class GetStadiumGroupsByFilterQuery : BaseQuery, IRequest<List<StadiumGroupDto>>
{
    public string? Q { get; set; }
}