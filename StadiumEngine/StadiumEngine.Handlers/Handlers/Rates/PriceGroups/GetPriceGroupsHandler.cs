using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Facades.Rates;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Rates.PriceGroups;
using StadiumEngine.Handlers.Queries.Rates;

namespace StadiumEngine.Handlers.Handlers.Rates.PriceGroups;

internal sealed class GetPriceGroupsHandler : BaseRequestHandler<GetPriceGroupsQuery, List<PriceGroupDto>>
{
    private readonly IPriceGroupFacade _priceGroupFacade;

    public GetPriceGroupsHandler(
        IPriceGroupFacade priceGroupFacade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _priceGroupFacade = priceGroupFacade;
    }
    
    public override async ValueTask<List<PriceGroupDto>> Handle(GetPriceGroupsQuery request, CancellationToken cancellationToken)
    {
        var priceGroups = await _priceGroupFacade.GetByStadiumId(_currentStadiumId);

        var priceGroupsDto = Mapper.Map<List<PriceGroupDto>>(priceGroups);

        return priceGroupsDto;
    }
}