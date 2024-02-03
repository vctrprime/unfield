using StadiumEngine.Domain.Entities.Dashboard;

namespace StadiumEngine.Domain.Repositories.Dashboard;

public interface IStadiumDashboardRepository
{
    Task AddAsync( StadiumDashboard dashboard );
}