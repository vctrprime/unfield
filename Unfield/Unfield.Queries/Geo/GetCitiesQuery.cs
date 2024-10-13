using Mediator;
using Unfield.DTO.Geo;

namespace Unfield.Queries.Geo;

public class GetCitiesQuery : BaseQuery, IRequest<List<CityDto>>
{
    public string? Q { get; set; }
}