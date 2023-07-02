using Mediator;
using StadiumEngine.DTO.Admin;

namespace StadiumEngine.Queries.Admin;

public sealed class GetLegalsByFilterQuery : BaseQuery, IRequest<List<LegalDto>>
{
    public string? Q { get; set; }
}