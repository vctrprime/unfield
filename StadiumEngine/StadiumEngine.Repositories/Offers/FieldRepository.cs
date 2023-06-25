using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Offers;

internal class FieldRepository : BaseRepository<Field>, IFieldRepository
{
    public FieldRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<List<Field>> GetAllAsync( int stadiumId ) =>
        await Entities
            .Where( f => f.StadiumId == stadiumId && !f.IsDeleted )
            .Include( f => f.SportKinds )
            .Include( f => f.Images )
            .Include( f => f.ChildFields.Where( cf => !cf.IsDeleted ) )
            .Include( f => f.PriceGroup )
            .Include( f => f.BreakFields )
            .ThenInclude( bf => bf.Break )
            .ToListAsync();

    public async Task<List<Field>> GetForCityAsync( int cityId, string? q ) =>
        await Entities
            .Include( f => f.SportKinds )
            .Include( f => f.Images )
            .Include( f => f.Stadium )
            .ThenInclude( s => s.City )
            .Include( f => f.BreakFields )
            .ThenInclude( bf => bf.Break )
            .Where( f => 
                f.Stadium.CityId == cityId 
                && f.IsActive
                && !f.IsDeleted 
                && f.Stadium.Name.ToLower().Contains( (q ?? String.Empty).ToLower() )
                && f.Stadium.Address.ToLower().Contains( (q ?? String.Empty).ToLower() )
                )
            .ToListAsync();

    public async Task<Field?> GetAsync( int fieldId, int stadiumId ) =>
        await Entities
            .Include( f => f.Stadium )
            .Include( f => f.SportKinds )
            .Include( f => f.Images )
            .Include( f => f.ChildFields.Where( cf => !cf.IsDeleted ) )
            .Include( f => f.PriceGroup )
            .FirstOrDefaultAsync( f => f.Id == fieldId && f.StadiumId == stadiumId && !f.IsDeleted );
    
    public async Task<Field?> GetAsync( int fieldId ) =>
        await Entities
            .Include( f => f.Stadium )
            .ThenInclude( s => s.City )
            .Include( f => f.SportKinds )
            .Include( f => f.Images )
            .Include( f => f.BreakFields )
            .ThenInclude( bf => bf.Break )
            .FirstOrDefaultAsync( f => f.Id == fieldId && !f.IsDeleted && f.IsActive );

    public new void Add( Field field ) => base.Add( field );

    public new void Update( Field field ) => base.Update( field );

    public new void Remove( Field field )
    {
        field.IsDeleted = true;
        base.Update( field );
    }
}