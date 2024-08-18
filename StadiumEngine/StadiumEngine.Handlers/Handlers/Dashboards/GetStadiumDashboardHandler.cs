using AutoMapper;
using Newtonsoft.Json;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Dashboard;
using StadiumEngine.Domain.Services.Core.Dashboard;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Dashboards;
using StadiumEngine.Queries.Dashboards;

namespace StadiumEngine.Handlers.Handlers.Dashboards;

internal sealed class GetStadiumDashboardHandler : BaseRequestHandler<GetStadiumDashboardQuery, StadiumDashboardDto>
{
    private readonly IStadiumDashboardQueryService _queryService;

    public GetStadiumDashboardHandler(
        IStadiumDashboardQueryService queryService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _queryService = queryService;
    }

    public override async ValueTask<StadiumDashboardDto> Handle( GetStadiumDashboardQuery request,
        CancellationToken cancellationToken )
    {
        StadiumDashboard? dashboard = await _queryService.GetAsync( _currentStadiumId );

        if ( dashboard is null )
        {
            throw new DomainException( ErrorsKeys.StadiumDashboardNotFound );
        }
        
        StadiumDashboardDto dashboardDto = Mapper.Map<StadiumDashboardDto>( dashboard );

        return dashboardDto;
    }
}