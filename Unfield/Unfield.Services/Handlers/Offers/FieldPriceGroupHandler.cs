using Unfield.Common;
using Unfield.Common.Exceptions;
using Unfield.Domain.Entities.Offers;
using Unfield.Domain.Entities.Rates;
using Unfield.Domain.Repositories.Rates;

namespace Unfield.Services.Handlers.Offers;

internal class FieldPriceGroupHandler : IFieldPriceGroupHandler
{
    private readonly IPriceGroupRepository _priceGroupRepository;
    private readonly IPriceRepository _priceRepository;

    public FieldPriceGroupHandler( IPriceGroupRepository priceGroupRepository, IPriceRepository priceRepository )
    {
        _priceGroupRepository = priceGroupRepository;
        _priceRepository = priceRepository;
    }


    public async Task HandleAsync( Field field, int? userId )
    {
        if ( !field.PriceGroupId.HasValue )
        {
            return;
        }
        
        PriceGroup? priceGroup = await _priceGroupRepository.GetAsync( field.PriceGroupId.Value, field.StadiumId );
        
        if ( priceGroup == null )
        {
            throw new DomainException( ErrorsKeys.PriceGroupNotFound );
        }

        IEnumerable<Field> fields = priceGroup.Fields.Where( f => f.Id != field.Id ).ToList();
        if ( !fields.Any() )
        {
            return;
        }
        
        int priceGroupFirstFieldId = fields.First().Id;

        List<Price> prices = await _priceRepository.GetAllAsync( field.StadiumId );

        List<Price> currentPrices = prices.Where( p => p.FieldId == field.Id ).ToList();
        if ( currentPrices.Any() )
        {
            _priceRepository.Remove( currentPrices );
        }
        
        prices = prices.Where( x => x.FieldId == priceGroupFirstFieldId ).ToList();

        
        IEnumerable<Price> fieldPrices = prices.Select( p => new Price
        {
            FieldId = field.Id,
            Currency = p.Currency,
            TariffDayIntervalId = p.TariffDayIntervalId,
            Value = p.Value,
            UserCreatedId = userId
        } );
        
        _priceRepository.Add( fieldPrices );

    }
}