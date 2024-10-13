using Unfield.Domain;
using Unfield.Domain.Entities.Dashboard;
using Unfield.Domain.Repositories.Dashboard;
using Unfield.Domain.Services.Core.Dashboard;

namespace Unfield.Services.Core.Dashboard;

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