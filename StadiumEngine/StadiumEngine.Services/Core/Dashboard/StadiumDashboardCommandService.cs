using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Dashboard;
using StadiumEngine.Domain.Repositories.Dashboard;
using StadiumEngine.Domain.Services.Core.Dashboard;

namespace StadiumEngine.Services.Core.Dashboard;

internal class StadiumDashboardCommandService : IStadiumDashboardCommandService
{
    private readonly IStadiumDashboardRepository _repository;
    private readonly IArchiveUnitOfWork _archiveUnitOfWork;

    public StadiumDashboardCommandService( IStadiumDashboardRepository repository, IArchiveUnitOfWork archiveUnitOfWork )
    {
        _repository = repository;
        _archiveUnitOfWork = archiveUnitOfWork;
    }

    public async Task AddAsync( StadiumDashboard dashboardData )
    { 
        await _repository.AddAsync( dashboardData );
        await _archiveUnitOfWork.SaveChangesAsync();
    }

    public async Task<int> DeleteByDateAsync( DateTime date ) => 
        await _repository.DeleteByDateAsync( date );
}