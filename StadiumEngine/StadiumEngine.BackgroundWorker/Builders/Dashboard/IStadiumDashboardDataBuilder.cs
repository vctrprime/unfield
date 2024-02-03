using StadiumEngine.Domain.Services.Models.Dashboard;

namespace StadiumEngine.BackgroundWorker.Builders.Dashboard;

internal interface IStadiumDashboardDataBuilder
{
    Task<StadiumDashboardResult> BuildAsync( int stadiumId, DateTime date );
}