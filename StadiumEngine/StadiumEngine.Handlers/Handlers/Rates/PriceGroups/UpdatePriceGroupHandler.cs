using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Facades.Rates;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Rates.PriceGroups;
using StadiumEngine.Handlers.Commands.Rates.PriceGroups;

namespace StadiumEngine.Handlers.Handlers.Rates.PriceGroups;

internal sealed class UpdatePriceGroupHandler : BaseCommandHandler<UpdatePriceGroupCommand, UpdatePriceGroupDto>
{
    private readonly IPriceGroupFacade _priceGroupFacade;

    public UpdatePriceGroupHandler(
        IPriceGroupFacade priceGroupFacade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _priceGroupFacade = priceGroupFacade;
    }

    protected override async ValueTask<UpdatePriceGroupDto> HandleCommand(UpdatePriceGroupCommand request, CancellationToken cancellationToken)
    {
        var priceGroup = await _priceGroupFacade.GetByPriceGroupId(request.Id, _currentStadiumId);

        if (priceGroup == null) throw new DomainException(ErrorsKeys.PriceGroupNotFound);
        
        priceGroup.Name = request.Name;
        priceGroup.Description = request.Description;
        priceGroup.IsActive = request.IsActive;
        priceGroup.UserModifiedId = _userId;
        
        _priceGroupFacade.UpdatePriceGroup(priceGroup);
        
        return new UpdatePriceGroupDto();
    }
}