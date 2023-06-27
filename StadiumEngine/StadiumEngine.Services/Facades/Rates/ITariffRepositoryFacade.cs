using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Services.Facades.Rates;

public interface ITariffRepositoryFacade
{
    void AddTariff( Tariff tariff );
    void UpdateTariff( Tariff tariff );
    Task<Tariff?> GetTariffAsync( int tariffId, int stadiumId );
    void RemoveTariff( Tariff tariff );
    
    void AddTariffDayInterval( TariffDayInterval tariffDayInterval );
    void RemoveTariffDayIntervals( IEnumerable<TariffDayInterval> tariffDayIntervals );
    
    Task<DayInterval?> GetDayIntervalAsync( string start, string end );
    void AddDayInterval( DayInterval dayInterval );
    
    void AddPromoCode( PromoCode promoCode );
    void UpdatePromoCode( PromoCode promoCode );
    void RemovePromoCodes( IEnumerable<PromoCode> promoCodes );
}