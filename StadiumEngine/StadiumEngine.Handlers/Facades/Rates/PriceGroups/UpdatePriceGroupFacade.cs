using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Services.Core.Rates;
using StadiumEngine.DTO.Rates.PriceGroups;
using StadiumEngine.Commands.Rates.PriceGroups;

namespace StadiumEngine.Handlers.Facades.Rates.PriceGroups;

internal class UpdatePriceGroupFacade : IUpdatePriceGroupFacade
{
    private readonly IPriceGroupCommandService _commandService;
    private readonly IPriceGroupQueryService _queryService;

    public UpdatePriceGroupFacade( IPriceGroupQueryService queryService, IPriceGroupCommandService commandService )
    {
        _queryService = queryService;
        _commandService = commandService;
    }

    public async Task<UpdatePriceGroupDto> UpdateAsync( UpdatePriceGroupCommand request, int stadiumId, int userId )
    {
        PriceGroup? priceGroup = await _queryService.GetByPriceGroupIdAsync( request.Id, stadiumId );

        if ( priceGroup == null )
        {
            throw new DomainException( ErrorsKeys.PriceGroupNotFound );
        }

        priceGroup.Name = request.Name;
        priceGroup.Description = request.Description;
        priceGroup.IsActive = request.IsActive;
        priceGroup.UserModifiedId = userId;

        _commandService.UpdatePriceGroup( priceGroup );

        return new UpdatePriceGroupDto();
    }
}