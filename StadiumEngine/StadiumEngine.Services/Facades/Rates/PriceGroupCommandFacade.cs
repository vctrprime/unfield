using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Repositories.Rates;
using StadiumEngine.Domain.Services.Facades.Rates;

namespace StadiumEngine.Services.Facades.Rates;

internal class PriceGroupCommandFacade : IPriceGroupCommandFacade
{
    private readonly IFieldRepository _fieldRepository;
    private readonly IPriceGroupRepository _priceGroupRepository;

    public PriceGroupCommandFacade( IPriceGroupRepository priceGroupRepository, IFieldRepository fieldRepository )
    {
        _priceGroupRepository = priceGroupRepository;
        _fieldRepository = fieldRepository;
    }

    public void AddPriceGroup( PriceGroup priceGroup )
    {
        _priceGroupRepository.Add( priceGroup );
    }

    public void UpdatePriceGroup( PriceGroup priceGroup )
    {
        _priceGroupRepository.Update( priceGroup );
        if (!priceGroup.IsActive) ResetFieldsPriceGroup( priceGroup );
    }

    public async Task DeletePriceGroup( int priceGroupId, int stadiumId )
    {
        var priceGroup = await _priceGroupRepository.Get( priceGroupId, stadiumId );

        if (priceGroup == null) throw new DomainException( ErrorsKeys.PriceGroupNotFound );

        _priceGroupRepository.Remove( priceGroup );

        ResetFieldsPriceGroup( priceGroup );
    }

    private void ResetFieldsPriceGroup( PriceGroup priceGroup )
    {
        foreach (var field in priceGroup.Fields)
        {
            field.PriceGroupId = null;
            field.UserModifiedId = priceGroup.UserModifiedId;
            _fieldRepository.Update( field );
        }
    }
}