using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.Dashboard;
using StadiumEngine.Domain.Repositories.Dashboard;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Dashboard;

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