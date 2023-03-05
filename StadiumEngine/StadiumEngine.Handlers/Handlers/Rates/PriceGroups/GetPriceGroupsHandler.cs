using AutoMapper;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Services.Facades.Rates;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Rates.PriceGroups;
using StadiumEngine.Handlers.Queries.Rates;

namespace StadiumEngine.Handlers.Handlers.Rates.PriceGroups;

internal sealed class GetPriceGroupsHandler : BaseRequestHandler<GetPriceGroupsQuery, List<PriceGroupDto>>
{
    private readonly IPriceGroupQueryFacade _priceGroupFacade;

    public GetPriceGroupsHandler(
        IPriceGroupQueryFacade priceGroupFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _priceGroupFacade = priceGroupFacade;
    }

    public override async ValueTask<List<PriceGroupDto>> Handle( GetPriceGroupsQuery request,
        CancellationToken cancellationToken )
    {
        List<PriceGroup> priceGroups = await _priceGroupFacade.GetByStadiumId( _currentStadiumId );

        List<PriceGroupDto>? priceGroupsDto = Mapper.Map<List<PriceGroupDto>>( priceGroups );

        return priceGroupsDto;
    }
}