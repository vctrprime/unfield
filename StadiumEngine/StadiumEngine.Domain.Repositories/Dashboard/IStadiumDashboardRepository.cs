using StadiumEngine.Domain.Entities.Dashboard;

namespace StadiumEngine.Domain.Repositories.Dashboard;

public interface IStadiumDashboardRepository
{
    Task<StadiumDashboard?> GetAsync( int stadiumId );
    Task AddAsync( StadiumDashboard dashboard );
    Task<int> DeleteByDateAsync( DateTime date );
}