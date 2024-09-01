using Microsoft.Extensions.Logging;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.Domain.Services.Core.Dashboard;
using StadiumEngine.Jobs.Background.Dashboard;

namespace StadiumEngine.Jobs.Recurring.Dashboard;

internal class CalculateStadiumDashboardJob : DefaultJob, ICalculateStadiumDashboardJob
{
    private readonly IStadiumQueryService _stadiumQueryService;
    private readonly IDashboardQueueManager _dashboardQueueManager;

    public CalculateStadiumDashboardJob(
        IStadiumQueryService stadiumQueryService,
        IDashboardQueueManager dashboardQueueManager,
        ILogger<CalculateStadiumDashboardJob> logger ) : base( logger, "CalculateStadiumDashboard" )
    {
        _stadiumQueryService = stadiumQueryService;
        _dashboardQueueManager = dashboardQueueManager;
    }

    public async Task Calculate()
    {
        LogInfo( "Start job" );

        int skip = 0;
        const int take = 50;

        List<Stadium> stadiums = new List<Stadium>();

        while ( skip == 0 || stadiums.Any() )
        {
            LogInfo( $"Fetch stadiums ( skip: {skip}, take: {take} )" );
            stadiums = await _stadiumQueryService.GetAsync( skip, take );

            LogInfo( $"Fetched {stadiums.Count} stadiums" );
            foreach ( Stadium stadium in stadiums )
            {
                _dashboardQueueManager.EnqueueCalculateStadiumDashboard( stadium.Id );
            }

            skip += stadiums.Count;
        }

        LogInfo( "Finish enqueued stadiums dashboards" );

        LogInfo( "Finish job" );
    }
}