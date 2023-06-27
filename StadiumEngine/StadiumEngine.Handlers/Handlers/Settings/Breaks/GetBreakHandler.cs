using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Services.Facades.Settings;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Settings.Breaks;
using StadiumEngine.Queries.Settings.Breaks;

namespace StadiumEngine.Handlers.Handlers.Settings.Breaks;

internal sealed class GetBreakHandler : BaseRequestHandler<GetBreakQuery, BreakDto>
{
    private readonly IBreakQueryFacade _breakFacade;

    public GetBreakHandler(
        IBreakQueryFacade breakFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _breakFacade = breakFacade;
    }

    public override async ValueTask<BreakDto> Handle( GetBreakQuery request,
        CancellationToken cancellationToken )
    {
        Break? @break = await _breakFacade.GetByBreakIdAsync( request.BreakId, _currentStadiumId );

        if ( @break == null )
        {
            throw new DomainException( ErrorsKeys.BreakNotFound );
        }

        BreakDto tariffDto = Mapper.Map<BreakDto>( @break );

        return tariffDto;
    }
}