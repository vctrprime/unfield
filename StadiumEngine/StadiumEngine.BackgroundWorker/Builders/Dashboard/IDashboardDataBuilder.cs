using StadiumEngine.Domain.Services.Models.Dashboard;

namespace StadiumEngine.BackgroundWorker.Builders.Dashboard;

internal interface IDashboardDataBuilder
{
    Task<DashboardData> BuildAsync( int stadiumId, DateTime date );
}