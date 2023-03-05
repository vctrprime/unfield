using Mediator;
using StadiumEngine.DTO.Admin;

namespace StadiumEngine.Handlers.Queries.Admin;

public sealed class GetLegalsByFilterQuery : IRequest<List<LegalDto>>
{
    public string SearchString { get; set; }

    public GetLegalsByFilterQuery( string searchString )
    {
        SearchString = searchString;
    }
}