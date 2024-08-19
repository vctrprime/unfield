namespace StadiumEngine.Jobs.Background.Dashboard;

public interface IDashboardQueueManager
{
    void EnqueueCalculateStadiumDashboard( int stadiumId );
}