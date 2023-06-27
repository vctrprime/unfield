using AutoMapper;
using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Services.Application.Settings;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Settings.Breaks;
using StadiumEngine.Queries.Settings.Breaks;

namespace StadiumEngine.Handlers.Handlers.Settings.Breaks;

internal sealed class GetBreaksHandler : BaseRequestHandler<GetBreaksQuery, List<BreakDto>>
{
    private readonly IBreakQueryService _queryService;

    public GetBreaksHandler(
        IBreakQueryService queryService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _queryService = queryService;
    }

    public override async ValueTask<List<BreakDto>> Handle( GetBreaksQuery request,
        CancellationToken cancellationToken )
    {
        List<Break> breaks = await _queryService.GetByStadiumIdAsync( _currentStadiumId );

        List<BreakDto> breaksDto = Mapper.Map<List<BreakDto>>( breaks );
        
        return breaksDto;
    }
}