using Unfield.Domain.Entities.Dashboard;

namespace Unfield.Domain.Services.Core.Dashboard;

public interface IStadiumDashboardCommandService
{
    Task AddAsync( StadiumDashboard dashboardData );
    Task<int> DeleteByDateAsync( DateTime date );
}