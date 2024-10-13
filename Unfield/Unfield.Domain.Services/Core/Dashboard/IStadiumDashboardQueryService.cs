using Unfield.Domain.Entities.Dashboard;

namespace Unfield.Domain.Services.Core.Dashboard;

public interface IStadiumDashboardQueryService
{
    Task<StadiumDashboard?> GetAsync( int stadiumId );
}