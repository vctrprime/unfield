using Unfield.Common;
using Unfield.Common.Exceptions;
using Unfield.Domain.Entities.Offers;
using Unfield.Domain.Entities.Rates;
using Unfield.Domain.Repositories.Offers;
using Unfield.Domain.Repositories.Rates;
using Unfield.Domain.Services.Core.Rates;

namespace Unfield.Services.Core.Rates;

internal class PriceGroupCommandService : IPriceGroupCommandService
{
    private readonly IFieldRepository _fieldRepository;
    private readonly IPriceGroupRepository _priceGroupRepository;

    public PriceGroupCommandService( IPriceGroupRepository priceGroupRepository, IFieldRepository fieldRepository )
    {
        _priceGroupRepository = priceGroupRepository;
        _fieldRepository = fieldRepository;
    }

    public void AddPriceGroup( PriceGroup priceGroup ) => _priceGroupRepository.Add( priceGroup );

    public void UpdatePriceGroup( PriceGroup priceGroup )
    {
        _priceGroupRepository.Update( priceGroup );
        if ( !priceGroup.IsActive )
        {
            ResetFieldsPriceGroup( priceGroup );
        }
    }

    public async Task DeletePriceGroupAsync( int priceGroupId, int stadiumId )
    {
        PriceGroup? priceGroup = await _priceGroupRepository.GetAsync( priceGroupId, stadiumId );

        if ( priceGroup == null )
        {
            throw new DomainException( ErrorsKeys.PriceGroupNotFound );
        }

        _priceGroupRepository.Remove( priceGroup );

        ResetFieldsPriceGroup( priceGroup );
    }

    private void ResetFieldsPriceGroup( PriceGroup priceGroup )
    {
        foreach ( Field? field in priceGroup.Fields )
        {
            field.PriceGroupId = null;
            field.UserModifiedId = priceGroup.UserModifiedId;
            _fieldRepository.Update( field );
        }
    }
}