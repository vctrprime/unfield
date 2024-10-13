using Unfield.Domain.Entities.Dashboard;

namespace Unfield.Domain.Repositories.Dashboard;

public interface IStadiumDashboardRepository
{
    Task<StadiumDashboard?> GetAsync( int stadiumId );
    Task AddAsync( StadiumDashboard dashboard );
    Task<int> DeleteByDateAsync( DateTime date );
}