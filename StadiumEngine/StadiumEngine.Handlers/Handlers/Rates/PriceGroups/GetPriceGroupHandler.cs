using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Services.Facades.Rates;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Rates.PriceGroups;
using StadiumEngine.Queries.Rates.PriceGroups;

namespace StadiumEngine.Handlers.Handlers.Rates.PriceGroups;

internal sealed class GetPriceGroupHandler : BaseRequestHandler<GetPriceGroupQuery, PriceGroupDto>
{
    private readonly IPriceGroupQueryFacade _priceGroupFacade;

    public GetPriceGroupHandler(
        IPriceGroupQueryFacade priceGroupFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _priceGroupFacade = priceGroupFacade;
    }

    public override async ValueTask<PriceGroupDto> Handle( GetPriceGroupQuery request,
        CancellationToken cancellationToken )
    {
        PriceGroup? priceGroup = await _priceGroupFacade.GetByPriceGroupId( request.PriceGroupId, _currentStadiumId );

        if ( priceGroup == null )
        {
            throw new DomainException( ErrorsKeys.PriceGroupNotFound );
        }

        PriceGroupDto? priceGroupDto = Mapper.Map<PriceGroupDto>( priceGroup );

        return priceGroupDto;
    }
}