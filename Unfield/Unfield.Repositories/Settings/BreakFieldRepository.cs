using Unfield.Domain.Entities.Settings;
using Unfield.Domain.Repositories.Settings;
using Unfield.Repositories.Infrastructure.Contexts;

namespace Unfield.Repositories.Settings;

internal class BreakFieldRepository : BaseRepository<BreakField>, IBreakFieldRepository
{
    public BreakFieldRepository( MainDbContext context ) : base( context )
    {
    }

    public new void Remove( IEnumerable<BreakField> breakFields ) => base.Remove( breakFields );

    public new void Add( IEnumerable<BreakField> breakFields ) => base.Add( breakFields );

}