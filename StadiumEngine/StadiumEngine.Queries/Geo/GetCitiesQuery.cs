using Mediator;
using StadiumEngine.DTO.Geo;

namespace StadiumEngine.Queries.Geo;

public class GetCitiesQuery  : IRequest<List<CityDto>>
{
    public GetCitiesQuery( string q )
    {
        Q = q;
    }

    public string Q { get; }
}