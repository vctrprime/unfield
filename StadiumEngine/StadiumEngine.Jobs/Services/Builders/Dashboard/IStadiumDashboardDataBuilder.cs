using StadiumEngine.Domain.Entities.Dashboard;

namespace StadiumEngine.Jobs.Services.Builders.Dashboard;

internal interface IStadiumDashboardDataBuilder
{
    Task<StadiumDashboard> BuildAsync( int stadiumId, DateTime date );
}