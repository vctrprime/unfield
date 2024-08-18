using StadiumEngine.BackgroundWorker.Builders.Dashboard;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Entities.Dashboard;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.Domain.Services.Core.Dashboard;

namespace StadiumEngine.BackgroundWorker.Jobs.Dashboard;

internal class DashboardCalculatorJob : DefaultJob, IDashboardCalculatorJob
{
    private readonly IStadiumQueryService _stadiumQueryService;
    private readonly IStadiumDashboardDataBuilder _builder;
    private readonly IStadiumDashboardCommandService _commandService;

    public DashboardCalculatorJob(
        IStadiumQueryService stadiumQueryService,
        IStadiumDashboardDataBuilder builder,
        IStadiumDashboardCommandService commandService,
        ILogger<DashboardCalculatorJob> logger ) : base( logger, "DashboardCalculator" )
    {
        _stadiumQueryService = stadiumQueryService;
        _builder = builder;
        _commandService = commandService;
    }

    public async Task Calculate()
    {
        LogInfo( "Start job" );

        await CalculateStadiumsDashboardsAsync();

        LogInfo( "Finish job" );
    }

    private async Task CalculateStadiumsDashboardsAsync()
    {
        LogInfo( "Start calculate stadiums dashboards" );

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
                LogInfo( $"Start calculate stadium {stadium.Id}" );
                try
                {
                    StadiumDashboard data = await _builder.BuildAsync( stadium.Id, DateTime.Now );
                    await _commandService.AddAsync( data );
                }
                catch ( Exception e )
                {
                    LogError( $"Stadium {stadium.Id} error", e );
                }


                LogInfo( $"Finish calculate stadium {stadium.Id}" );
            }

            skip += stadiums.Count;
        }

        LogInfo( "Finish calculate stadiums dashboards" );
    }
}