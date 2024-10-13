using AutoMapper;
using Unfield.Common;
using Unfield.Common.Exceptions;
using Unfield.Domain.Entities.Rates;
using Unfield.Domain.Services.Core.Rates;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Rates.Tariffs;
using Unfield.Queries.Rates.Tariffs;

namespace Unfield.Handlers.Handlers.Rates.Tariffs;

internal sealed class GetTariffHandler : BaseRequestHandler<GetTariffQuery, TariffDto>
{
    private readonly ITariffQueryService _queryService;

    public GetTariffHandler(
        ITariffQueryService queryService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _queryService = queryService;
    }

    public override async ValueTask<TariffDto> Handle( GetTariffQuery request,
        CancellationToken cancellationToken )
    {
        Tariff? tariff = await _queryService.GetByTariffIdAsync( request.TariffId, _currentStadiumId );

        if ( tariff == null )
        {
            throw new DomainException( ErrorsKeys.TariffNotFound );
        }

        TariffDto tariffDto = Mapper.Map<TariffDto>( tariff );
        tariffDto.DayIntervals = tariffDto.DayIntervals
            .OrderBy( x => Int32.Parse( x.Interval[ 0 ].Replace( ":", "" ) ) ).ToList();

        return tariffDto;
    }
}