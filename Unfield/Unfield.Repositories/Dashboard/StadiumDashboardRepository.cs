using Microsoft.EntityFrameworkCore;
using Unfield.Domain.Entities.Dashboard;
using Unfield.Domain.Repositories.Dashboard;
using Unfield.Repositories.Infrastructure.Contexts;

namespace Unfield.Repositories.Dashboard;

internal class StadiumDashboardRepository : BaseRepository<StadiumDashboard>, IStadiumDashboardRepository
{
    public StadiumDashboardRepository( ArchiveDbContext context ) : base( context )
    {
    }

    public Task<StadiumDashboard?> GetAsync( int stadiumId ) =>
        Entities
            .Where( x => x.StadiumId == stadiumId )
            .OrderByDescending( x => x.DateCreated )
            .FirstOrDefaultAsync();

    public async Task AddAsync( StadiumDashboard dashboard )
    {
        Add( dashboard );
        await Commit();
    }

    public async Task<int> DeleteByDateAsync( DateTime date )
    {
        int rows = await Entities
            .Where( x => x.DateCreated < date )
            .ExecuteDeleteAsync();
        
        return rows;
    }
}