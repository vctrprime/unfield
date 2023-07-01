using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Services.Core.Rates;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Rates.PriceGroups;
using StadiumEngine.Queries.Rates.PriceGroups;

namespace StadiumEngine.Handlers.Handlers.Rates.PriceGroups;

internal sealed class GetPriceGroupHandler : BaseRequestHandler<GetPriceGroupQuery, PriceGroupDto>
{
    private readonly IPriceGroupQueryService _queryService;

    public GetPriceGroupHandler(
        IPriceGroupQueryService queryService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _queryService = queryService;
    }

    public override async ValueTask<PriceGroupDto> Handle( GetPriceGroupQuery request,
        CancellationToken cancellationToken )
    {
        PriceGroup? priceGroup = await _queryService.GetByPriceGroupIdAsync( request.PriceGroupId, _currentStadiumId );

        if ( priceGroup == null )
        {
            throw new DomainException( ErrorsKeys.PriceGroupNotFound );
        }

        PriceGroupDto? priceGroupDto = Mapper.Map<PriceGroupDto>( priceGroup );

        return priceGroupDto;
    }
}