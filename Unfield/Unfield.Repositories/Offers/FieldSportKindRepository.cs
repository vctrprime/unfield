using Unfield.Domain.Entities.Offers;
using Unfield.Domain.Repositories.Offers;
using Unfield.Repositories.Infrastructure.Contexts;

namespace Unfield.Repositories.Offers;

internal class OffersSportKindRepository : BaseRepository<OffersSportKind>, IOffersSportKindRepository
{
    public OffersSportKindRepository( MainDbContext context ) : base( context )
    {
    }

    public new void Add( IEnumerable<OffersSportKind> sportKinds ) => base.Add( sportKinds );

    public new void Remove( IEnumerable<OffersSportKind> sportKinds ) => base.Remove( sportKinds );
}