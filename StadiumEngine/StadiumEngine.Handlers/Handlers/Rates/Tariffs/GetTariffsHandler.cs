using AutoMapper;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Services.Core.Rates;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Rates.Tariffs;
using StadiumEngine.Queries.Rates.Tariffs;

namespace StadiumEngine.Handlers.Handlers.Rates.Tariffs;

internal sealed class GetTariffsHandler : BaseRequestHandler<GetTariffsQuery, List<TariffDto>>
{
    private readonly ITariffQueryService _queryService;

    public GetTariffsHandler(
        ITariffQueryService queryService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _queryService = queryService;
    }

    public override async ValueTask<List<TariffDto>> Handle( GetTariffsQuery request,
        CancellationToken cancellationToken )
    {
        List<Tariff> tariffs = await _queryService.GetByStadiumIdAsync( _currentStadiumId );

        List<TariffDto> tariffsDto = Mapper.Map<List<TariffDto>>( tariffs );
        tariffsDto.ForEach(
            dto =>
            {
                dto.DayIntervals = dto.DayIntervals.OrderBy( x => Int32.Parse( x.Interval[ 0 ].Replace( ":", "" ) ) ).ToList();
            } );
        
        return tariffsDto;
    }
}