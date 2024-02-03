using StadiumEngine.Domain.Services.Models.Dashboard;

namespace StadiumEngine.BackgroundWorker.Builders.Dashboard;

internal class StadiumDashboardDataBuilder : IStadiumDashboardDataBuilder
{
    public async Task<StadiumDashboardResult> BuildAsync( int stadiumId, DateTime date )
    {
        Console.WriteLine( $"Build stadium {stadiumId}");
        return new StadiumDashboardResult
        {
            Data = new StadiumDashboardData()
        };
    }
}