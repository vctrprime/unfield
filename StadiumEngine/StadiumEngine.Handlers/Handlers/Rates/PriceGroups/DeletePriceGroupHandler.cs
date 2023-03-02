using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Facades.Rates;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Rates.PriceGroups;
using StadiumEngine.Handlers.Commands.Rates.PriceGroups;

namespace StadiumEngine.Handlers.Handlers.Rates.PriceGroups;

internal sealed class DeletePriceGroupHandler : BaseRequestHandler<DeletePriceGroupCommand, DeletePriceGroupDto>
{
    private readonly IPriceGroupFacade _priceGroupFacade;

    public DeletePriceGroupHandler(
        IPriceGroupFacade priceGroupFacade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _priceGroupFacade = priceGroupFacade;
    }

    public override async ValueTask<DeletePriceGroupDto> Handle(DeletePriceGroupCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await UnitOfWork.BeginTransaction();
            
            await _priceGroupFacade.DeletePriceGroup(request.PriceGroupId, _currentStadiumId);
            
            await UnitOfWork.CommitTransaction();
        }
        catch
        {
            await UnitOfWork.RollbackTransaction();
            throw;
        }
        
        return new DeletePriceGroupDto();
    }
}