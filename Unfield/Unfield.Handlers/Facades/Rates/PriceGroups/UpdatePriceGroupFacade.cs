using Unfield.Common;
using Unfield.Common.Exceptions;
using Unfield.Domain.Entities.Rates;
using Unfield.Domain.Services.Core.Rates;
using Unfield.DTO.Rates.PriceGroups;
using Unfield.Commands.Rates.PriceGroups;

namespace Unfield.Handlers.Facades.Rates.PriceGroups;

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