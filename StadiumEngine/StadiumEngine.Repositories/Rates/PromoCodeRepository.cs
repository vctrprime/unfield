using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Repositories.Rates;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Rates;

internal class PromoCodeRepository : BaseRepository<PromoCode>, IPromoCodeRepository
{
    public PromoCodeRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<PromoCode?> Get( int tariffId, string code ) =>
        await Entities.FirstOrDefaultAsync( x => x.TariffId == tariffId && x.Code.ToLower() == code );

    public new void Add( PromoCode promoCode ) => base.Add( promoCode );

    public new void Update( PromoCode promoCode ) => base.Update( promoCode );

    public new void Remove( IEnumerable<PromoCode> promoCodes ) => base.Remove( promoCodes );
}