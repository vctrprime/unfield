using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Services.Application.Settings;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Settings.Breaks;
using StadiumEngine.Queries.Settings.Breaks;

namespace StadiumEngine.Handlers.Handlers.Settings.Breaks;

internal sealed class GetBreakHandler : BaseRequestHandler<GetBreakQuery, BreakDto>
{
    private readonly IBreakQueryService _queryService;

    public GetBreakHandler(
        IBreakQueryService queryService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _queryService = queryService;
    }

    public override async ValueTask<BreakDto> Handle( GetBreakQuery request,
        CancellationToken cancellationToken )
    {
        Break? @break = await _queryService.GetByBreakIdAsync( request.BreakId, _currentStadiumId );

        if ( @break == null )
        {
            throw new DomainException( ErrorsKeys.BreakNotFound );
        }

        BreakDto tariffDto = Mapper.Map<BreakDto>( @break );

        return tariffDto;
    }
}