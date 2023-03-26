using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Facades.Rates;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Rates.PriceGroups;
using StadiumEngine.Commands.Rates.PriceGroups;

namespace StadiumEngine.Handlers.Handlers.Rates.PriceGroups;

internal sealed class DeletePriceGroupHandler : BaseCommandHandler<DeletePriceGroupCommand, DeletePriceGroupDto>
{
    private readonly IPriceGroupCommandFacade _priceGroupFacade;

    public DeletePriceGroupHandler(
        IPriceGroupCommandFacade priceGroupFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _priceGroupFacade = priceGroupFacade;
    }

    protected override async ValueTask<DeletePriceGroupDto> HandleCommandAsync( DeletePriceGroupCommand request,
        CancellationToken cancellationToken )
    {
        await _priceGroupFacade.DeletePriceGroupAsync( request.PriceGroupId, _currentStadiumId );
        return new DeletePriceGroupDto();
    }
}