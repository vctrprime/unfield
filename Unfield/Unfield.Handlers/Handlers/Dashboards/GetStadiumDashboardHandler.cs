using AutoMapper;
using Newtonsoft.Json;
using Unfield.Common;
using Unfield.Common.Exceptions;
using Unfield.Domain.Entities.Dashboard;
using Unfield.Domain.Services.Core.Dashboard;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Dashboards;
using Unfield.Queries.Dashboards;

namespace Unfield.Handlers.Handlers.Dashboards;

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