using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Services.Facades.Rates;
using StadiumEngine.DTO.Rates.PriceGroups;
using StadiumEngine.Handlers.Commands.Rates.PriceGroups;

namespace StadiumEngine.Handlers.Facades.Rates.PriceGroups;

internal class UpdatePriceGroupFacade : IUpdatePriceGroupFacade
{
    private readonly IPriceGroupQueryFacade _queryFacade;
    private readonly IPriceGroupCommandFacade _commandFacade;

    public UpdatePriceGroupFacade( IPriceGroupQueryFacade queryFacade, IPriceGroupCommandFacade commandFacade )
    {
        _queryFacade = queryFacade;
        _commandFacade = commandFacade;
    }

    public async Task<UpdatePriceGroupDto> Update( UpdatePriceGroupCommand request, int stadiumId, int userId )
    {
        var priceGroup = await _queryFacade.GetByPriceGroupId( request.Id, stadiumId );

        if (priceGroup == null) throw new DomainException( ErrorsKeys.PriceGroupNotFound );

        priceGroup.Name = request.Name;
        priceGroup.Description = request.Description;
        priceGroup.IsActive = request.IsActive;
        priceGroup.UserModifiedId = userId;

        _commandFacade.UpdatePriceGroup( priceGroup );

        return new UpdatePriceGroupDto();
    }
}