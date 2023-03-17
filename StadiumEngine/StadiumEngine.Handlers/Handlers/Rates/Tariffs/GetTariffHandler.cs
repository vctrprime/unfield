using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Services.Facades.Rates;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Rates.Tariffs;
using StadiumEngine.Queries.Rates.Tariffs;

namespace StadiumEngine.Handlers.Handlers.Rates.Tariffs;

internal sealed class GetTariffHandler : BaseRequestHandler<GetTariffQuery, TariffDto>
{
    private readonly ITariffQueryFacade _tariffFacade;

    public GetTariffHandler(
        ITariffQueryFacade tariffFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _tariffFacade = tariffFacade;
    }

    public override async ValueTask<TariffDto> Handle( GetTariffQuery request,
        CancellationToken cancellationToken )
    {
        Tariff? tariff = await _tariffFacade.GetByTariffId( request.TariffId, _currentStadiumId );

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