using Mediator;
using StadiumEngine.DTO.Admin;

namespace StadiumEngine.Queries.Admin;

public sealed class GetStadiumGroupsByFilterQuery : BaseQuery, IRequest<List<StadiumGroupDto>>
{
    public string? Q { get; set; }
}