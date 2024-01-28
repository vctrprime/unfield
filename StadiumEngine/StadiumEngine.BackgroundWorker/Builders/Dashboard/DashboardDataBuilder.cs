using StadiumEngine.Domain.Services.Models.Dashboard;

namespace StadiumEngine.BackgroundWorker.Builders.Dashboard;

internal class DashboardDataBuilder : IDashboardDataBuilder
{
    public async Task<DashboardData> BuildAsync( int stadiumId, DateTime date )
    {
        Console.WriteLine( $"Stadium {stadiumId}");
        return new DashboardData();
    }
}