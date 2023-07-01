using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Services.Core.Rates;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Rates.Tariffs;
using StadiumEngine.Queries.Rates.Tariffs;

namespace StadiumEngine.Handlers.Handlers.Rates.Tariffs;

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