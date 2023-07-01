using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Services.Core.Offers;
using StadiumEngine.Domain.Services.Core.Rates;
using StadiumEngine.DTO.Rates.Prices;

namespace StadiumEngine.Handlers.Facades.Rates.Prices;

internal class SetPricesFacade : ISetPricesFacade
{
    private readonly IPriceCommandService _priceCommandService;
    private readonly IPriceQueryService _priceQueryService;
    private readonly ITariffQueryService _tariffQueryService;
    private readonly IFieldQueryService _fieldQueryService;

    public SetPricesFacade( IPriceCommandService priceCommandService, IPriceQueryService priceQueryService,
        ITariffQueryService tariffQueryService, IFieldQueryService fieldQueryService )
    {
        _priceCommandService = priceCommandService;
        _priceQueryService = priceQueryService;
        _tariffQueryService = tariffQueryService;
        _fieldQueryService = fieldQueryService;
    }

    public async Task<SetPricesDto> SetPricesAsync( IEnumerable<Price> prices, int stadiumId, int userId )
    {
        List<Price> currentPrices = await _priceQueryService.GetByStadiumIdAsync( stadiumId );
        _priceCommandService.DeletePrices( currentPrices );

        List<Field> fields = await _fieldQueryService.GetByStadiumIdAsync( stadiumId );
        List<Tariff> tariffs = await _tariffQueryService.GetByStadiumIdAsync( stadiumId );

        List<Price> newPrices = new();
        foreach ( Price price in prices )
        {
            Field? field = fields.SingleOrDefault( x => x.Id == price.FieldId );
            if ( field == null )
            {
                throw new DomainException( ErrorsKeys.FieldNotFound );
            }
            
            if ( tariffs.SelectMany( x => x.TariffDayIntervals ).All( x => x.Id != price.TariffDayIntervalId ) )
            {
                throw new DomainException( ErrorsKeys.TariffNotFound );
            }

            if ( field.PriceGroupId.HasValue )
            {
                foreach ( Field samePriceGroupField in fields.Where(
                             x => x.Id != field.Id
                                  && x.PriceGroupId == field.PriceGroupId ) )
                {
                    if ( !newPrices.Any(
                            x => x.FieldId == samePriceGroupField.Id &&
                                 x.TariffDayIntervalId == price.TariffDayIntervalId ) )
                    {
                        newPrices.Add(
                            new Price
                            {
                                UserCreatedId = userId,
                                Currency = price.Currency,
                                TariffDayIntervalId = price.TariffDayIntervalId,
                                FieldId = samePriceGroupField.Id,
                                Value = price.Value
                            } );
                    }
                }
            }
            
            if ( newPrices.Any(
                    x => x.FieldId == price.FieldId && x.TariffDayIntervalId == price.TariffDayIntervalId ) )
            {
                continue;
            }

            price.UserCreatedId = userId;
            newPrices.Add( price );
        }

        _priceCommandService.AddPrices( newPrices );

        return new SetPricesDto();
    }
}