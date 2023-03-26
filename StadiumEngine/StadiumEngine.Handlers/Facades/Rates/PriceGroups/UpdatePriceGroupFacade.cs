using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Services.Facades.Rates;
using StadiumEngine.DTO.Rates.PriceGroups;
using StadiumEngine.Commands.Rates.PriceGroups;

namespace StadiumEngine.Handlers.Facades.Rates.PriceGroups;

internal class UpdatePriceGroupFacade : IUpdatePriceGroupFacade
{
    private readonly IPriceGroupCommandFacade _commandFacade;
    private readonly IPriceGroupQueryFacade _queryFacade;

    public UpdatePriceGroupFacade( IPriceGroupQueryFacade queryFacade, IPriceGroupCommandFacade commandFacade )
    {
        _queryFacade = queryFacade;
        _commandFacade = commandFacade;
    }

    public async Task<UpdatePriceGroupDto> UpdateAsync( UpdatePriceGroupCommand request, int stadiumId, int userId )
    {
        PriceGroup? priceGroup = await _queryFacade.GetByPriceGroupIdAsync( request.Id, stadiumId );

        if ( priceGroup == null )
        {
            throw new DomainException( ErrorsKeys.PriceGroupNotFound );
        }

        priceGroup.Name = request.Name;
        priceGroup.Description = request.Description;
        priceGroup.IsActive = request.IsActive;
        priceGroup.UserModifiedId = userId;

        _commandFacade.UpdatePriceGroup( priceGroup );

        return new UpdatePriceGroupDto();
    }
}