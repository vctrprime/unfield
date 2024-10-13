using Microsoft.EntityFrameworkCore;
using Unfield.Domain.Entities.Rates;
using Unfield.Domain.Repositories.Rates;
using Unfield.Repositories.Infrastructure.Contexts;

namespace Unfield.Repositories.Rates;

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