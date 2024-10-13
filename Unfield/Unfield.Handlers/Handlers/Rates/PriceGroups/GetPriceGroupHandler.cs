using AutoMapper;
using Unfield.Common;
using Unfield.Common.Exceptions;
using Unfield.Domain.Entities.Rates;
using Unfield.Domain.Services.Core.Rates;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Rates.PriceGroups;
using Unfield.Queries.Rates.PriceGroups;

namespace Unfield.Handlers.Handlers.Rates.PriceGroups;

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