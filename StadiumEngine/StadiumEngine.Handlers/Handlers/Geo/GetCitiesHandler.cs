using AutoMapper;
using StadiumEngine.Domain.Entities.Geo;
using StadiumEngine.Domain.Services.Application.Geo;
using StadiumEngine.DTO.Geo;
using StadiumEngine.Queries.Geo;

namespace StadiumEngine.Handlers.Handlers.Geo;

internal class GetCitiesHandler : BaseRequestHandler<GetCitiesQuery, List<CityDto>>
{
    private readonly ICityQueryService _queryService;

    public GetCitiesHandler( ICityQueryService queryService, IMapper mapper ) : base( mapper )
    {
        _queryService = queryService;
    }

    public override async ValueTask<List<CityDto>> Handle( GetCitiesQuery request, CancellationToken cancellationToken )
    {
        List<City> cities = await _queryService.GetCitiesByFilterAsync( request.Q ?? String.Empty );

        List<CityDto>? result = Mapper.Map<List<CityDto>>( cities );

        return result;
    }
}