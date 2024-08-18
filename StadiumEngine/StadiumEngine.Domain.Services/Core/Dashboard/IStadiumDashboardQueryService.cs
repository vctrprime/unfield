using StadiumEngine.Domain.Entities.Dashboard;

namespace StadiumEngine.Domain.Services.Core.Dashboard;

public interface IStadiumDashboardQueryService
{
    Task<StadiumDashboard?> GetAsync( int stadiumId );
}