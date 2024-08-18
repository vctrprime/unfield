using StadiumEngine.Domain.Entities.Dashboard;
namespace StadiumEngine.BackgroundWorker.Builders.Dashboard;

internal interface IStadiumDashboardDataBuilder
{
    Task<StadiumDashboard> BuildAsync( int stadiumId, DateTime date );
}