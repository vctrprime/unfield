using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Domain.Repositories.Rates;

public interface IPromoCodeRepository
{
    void Add( PromoCode promoCode );
    void Update( PromoCode promoCode );
    void Remove( IEnumerable<PromoCode> promoCodes );
}