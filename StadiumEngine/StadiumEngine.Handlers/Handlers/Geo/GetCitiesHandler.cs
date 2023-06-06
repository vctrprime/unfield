using AutoMapper;
using StadiumEngine.Domain.Entities.Geo;
using StadiumEngine.Domain.Services.Facades.Geo;
using StadiumEngine.DTO.Geo;
using StadiumEngine.Queries.Geo;

namespace StadiumEngine.Handlers.Handlers.Geo;

internal class GetCitiesHandler : BaseRequestHandler<GetCitiesQuery, List<CityDto>>
{
    private readonly ICityQueryFacade _facade;

    public GetCitiesHandler( ICityQueryFacade facade, IMapper mapper ) : base( mapper )
    {
        _facade = facade;
    }

    public override async ValueTask<List<CityDto>> Handle( GetCitiesQuery request, CancellationToken cancellationToken )
    {
        List<City> cities = await _facade.GetCitiesByFilterAsync( request.Q ?? String.Empty );

        List<CityDto>? result = Mapper.Map<List<CityDto>>( cities );

        return result;
    }
}