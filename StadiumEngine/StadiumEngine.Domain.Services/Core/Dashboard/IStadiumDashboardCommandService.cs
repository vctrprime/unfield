using StadiumEngine.Domain.Services.Models.Dashboard;

namespace StadiumEngine.Domain.Services.Core.Dashboard;

public interface IStadiumDashboardCommandService
{
    Task AddAsync( StadiumDashboardResult dashboardData );
}