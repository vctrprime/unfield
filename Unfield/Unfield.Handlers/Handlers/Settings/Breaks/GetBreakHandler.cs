using AutoMapper;
using Unfield.Common;
using Unfield.Common.Exceptions;
using Unfield.Domain.Entities.Settings;
using Unfield.Domain.Services.Core.Settings;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Settings.Breaks;
using Unfield.Queries.Settings.Breaks;

namespace Unfield.Handlers.Handlers.Settings.Breaks;

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