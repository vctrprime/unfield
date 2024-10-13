using AutoMapper;
using Unfield.Domain.Entities.Rates;
using Unfield.Domain.Services.Core.Rates;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Rates.Tariffs;
using Unfield.Queries.Rates.Tariffs;

namespace Unfield.Handlers.Handlers.Rates.Tariffs;

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