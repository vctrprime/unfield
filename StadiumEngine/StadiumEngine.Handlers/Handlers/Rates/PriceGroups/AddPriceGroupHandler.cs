using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Services.Facades.Rates;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Rates.PriceGroups;
using StadiumEngine.Handlers.Commands.Rates.PriceGroups;

namespace StadiumEngine.Handlers.Handlers.Rates.PriceGroups;

internal sealed class AddPriceGroupHandler : BaseCommandHandler<AddPriceGroupCommand, AddPriceGroupDto>
{
    private readonly IPriceGroupCommandFacade _priceGroupFacade;

    public AddPriceGroupHandler(
        IPriceGroupCommandFacade priceGroupFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _priceGroupFacade = priceGroupFacade;
    }

    protected override async ValueTask<AddPriceGroupDto> HandleCommand( AddPriceGroupCommand request,
        CancellationToken cancellationToken )
    {
        var priceGroup = Mapper.Map<PriceGroup>( request );
        priceGroup.StadiumId = _currentStadiumId;
        priceGroup.UserCreatedId = _userId;

        _priceGroupFacade.AddPriceGroup( priceGroup );

        return new AddPriceGroupDto();
    }
}