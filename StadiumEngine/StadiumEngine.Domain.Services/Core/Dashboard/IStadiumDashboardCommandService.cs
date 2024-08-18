using StadiumEngine.Domain.Entities.Dashboard;

namespace StadiumEngine.Domain.Services.Core.Dashboard;

public interface IStadiumDashboardCommandService
{
    Task AddAsync( StadiumDashboard dashboardData );
}