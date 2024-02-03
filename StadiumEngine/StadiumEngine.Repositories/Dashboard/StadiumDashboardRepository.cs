using StadiumEngine.Domain.Entities.Dashboard;
using StadiumEngine.Domain.Repositories.Dashboard;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Dashboard;

internal class StadiumDashboardRepository : BaseRepository<StadiumDashboard>, IStadiumDashboardRepository
{
    public StadiumDashboardRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task AddAsync( StadiumDashboard dashboard ) 
    {
        Add( dashboard );
        await Commit();
    }
}