using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Offers;

internal class OffersSportKindRepository : BaseRepository<OffersSportKind>, IOffersSportKindRepository
{
    public OffersSportKindRepository( MainDbContext context ) : base( context )
    {
    }

    public new void Add( IEnumerable<OffersSportKind> sportKinds )
    {
        base.Add( sportKinds );
    }

    public new void Remove( IEnumerable<OffersSportKind> sportKinds )
    {
        base.Remove( sportKinds );
    }
}