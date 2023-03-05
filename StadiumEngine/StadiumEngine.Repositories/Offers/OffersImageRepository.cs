using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Offers;

internal class OffersImageRepository : BaseRepository<OffersImage>, IOffersImageRepository
{
    public OffersImageRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<List<OffersImage>> GetForField( int fieldId )
    {
        return await Entities
            .Where( i => i.FieldId == fieldId )
            .ToListAsync();
    }

    public new void Add( OffersImage image )
    {
        base.Add( image );
    }

    public new void Update( OffersImage image )
    {
        base.Update( image );
    }

    public new void Remove( IEnumerable<OffersImage> images )
    {
        base.Remove( images );
    }
}