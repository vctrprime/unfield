using AutoMapper;
using Unfield.Domain.Entities.Settings;
using Unfield.Domain.Services.Core.Settings;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Settings.Breaks;
using Unfield.Queries.Settings.Breaks;

namespace Unfield.Handlers.Handlers.Settings.Breaks;

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