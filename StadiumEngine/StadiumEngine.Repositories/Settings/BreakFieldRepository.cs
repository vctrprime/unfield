using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Repositories.Settings;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Settings;

internal class BreakFieldRepository : BaseRepository<BreakField>, IBreakFieldRepository
{
    public BreakFieldRepository( MainDbContext context ) : base( context )
    {
    }

    public new void Remove( IEnumerable<BreakField> breakFields ) => base.Remove( breakFields );

    public new void Add( IEnumerable<BreakField> breakFields ) => base.Add( breakFields );

}