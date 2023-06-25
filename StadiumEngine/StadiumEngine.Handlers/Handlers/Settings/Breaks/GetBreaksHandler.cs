using AutoMapper;
using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Services.Facades.Settings;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Settings.Breaks;
using StadiumEngine.Queries.Settings.Breaks;

namespace StadiumEngine.Handlers.Handlers.Settings.Breaks;

internal sealed class GetBreaksHandler : BaseRequestHandler<GetBreaksQuery, List<BreakDto>>
{
    private readonly IBreakQueryFacade _breakFacade;

    public GetBreaksHandler(
        IBreakQueryFacade breakFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _breakFacade = breakFacade;
    }

    public override async ValueTask<List<BreakDto>> Handle( GetBreaksQuery request,
        CancellationToken cancellationToken )
    {
        List<Break> breaks = await _breakFacade.GetByStadiumIdAsync( _currentStadiumId );

        List<BreakDto> breaksDto = Mapper.Map<List<BreakDto>>( breaks );
        
        return breaksDto;
    }
}