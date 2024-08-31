namespace StadiumEngine.Domain.Services.Core.Dashboard;

public interface IDashboardQueueManager
{
    void EnqueueCalculateStadiumDashboard( int stadiumId );
}