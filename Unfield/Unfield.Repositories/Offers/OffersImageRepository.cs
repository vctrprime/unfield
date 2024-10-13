using Microsoft.EntityFrameworkCore;
using Unfield.Domain.Entities.Offers;
using Unfield.Domain.Repositories.Offers;
using Unfield.Repositories.Infrastructure.Contexts;

namespace Unfield.Repositories.Offers;

internal class OffersImageRepository : BaseRepository<OffersImage>, IOffersImageRepository
{
    public OffersImageRepository( MainDbContext context ) : base( context )
    {
    }
    
    public new void Add( OffersImage image ) => base.Add( image );

    public new void Update( OffersImage image ) => base.Update( image );

    public new void Remove( IEnumerable<OffersImage> images ) => base.Remove( images );
}