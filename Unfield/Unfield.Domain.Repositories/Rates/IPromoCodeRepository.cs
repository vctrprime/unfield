using Unfield.Domain.Entities.Rates;

namespace Unfield.Domain.Repositories.Rates;

public interface IPromoCodeRepository
{
    Task<PromoCode?> Get( int tariffId, string code );
    void Add( PromoCode promoCode );
    void Update( PromoCode promoCode );
    void Remove( IEnumerable<PromoCode> promoCodes );
}