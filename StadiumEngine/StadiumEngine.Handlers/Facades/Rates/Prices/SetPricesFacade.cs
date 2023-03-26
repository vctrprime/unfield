using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Facades.Rates;
using StadiumEngine.DTO.Rates.Prices;

namespace StadiumEngine.Handlers.Facades.Rates.Prices;

internal class SetPricesFacade : ISetPricesFacade
{
    private readonly IPriceCommandFacade _priceCommandFacade;
    private readonly IPriceQueryFacade _priceQueryFacade;
    private readonly ITariffQueryFacade _tariffQueryFacade;
    private readonly IFieldQueryFacade _fieldQueryFacade;

    public SetPricesFacade( IPriceCommandFacade priceCommandFacade, IPriceQueryFacade priceQueryFacade,
        ITariffQueryFacade tariffQueryFacade, IFieldQueryFacade fieldQueryFacade )
    {
        _priceCommandFacade = priceCommandFacade;
        _priceQueryFacade = priceQueryFacade;
        _tariffQueryFacade = tariffQueryFacade;
        _fieldQueryFacade = fieldQueryFacade;
    }

    public async Task<SetPricesDto> SetPricesAsync( IEnumerable<Price> prices, int stadiumId, int userId )
    {
        List<Price> currentPrices = await _priceQueryFacade.GetByStadiumIdAsync( stadiumId );
        _priceCommandFacade.DeletePrices( currentPrices );

        List<Field> fields = await _fieldQueryFacade.GetByStadiumIdAsync( stadiumId );
        List<Tariff> tariffs = await _tariffQueryFacade.GetByStadiumIdAsync( stadiumId );

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

        _priceCommandFacade.AddPrices( newPrices );

        return new SetPricesDto();
    }
}