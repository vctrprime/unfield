using Mediator;
using StadiumEngine.DTO.Geo;

namespace StadiumEngine.Queries.Geo;

public class GetCitiesQuery  : IRequest<List<CityDto>>
{
    public string? Q { get; set; }
}