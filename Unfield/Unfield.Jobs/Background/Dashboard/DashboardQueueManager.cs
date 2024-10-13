using Hangfire;
using Unfield.Domain.Services.Core.Dashboard;

namespace Unfield.Jobs.Background.Dashboard;

internal class DashboardQueueManager : IDashboardQueueManager
{
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly IStadiumDashboardCalculator _stadiumDashboardCalculator;

    public DashboardQueueManager( IBackgroundJobClient backgroundJobClient, IStadiumDashboardCalculator stadiumDashboardCalculator )
    {
        _backgroundJobClient = backgroundJobClient;
        _stadiumDashboardCalculator = stadiumDashboardCalculator;
    }

    public void EnqueueCalculateStadiumDashboard( int stadiumId ) => 
        _backgroundJobClient.Enqueue( () => _stadiumDashboardCalculator.CalculateAsync( stadiumId ) );
}