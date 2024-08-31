using Hangfire;
using Microsoft.Extensions.Logging;
using StadiumEngine.Domain.Entities.Dashboard;
using StadiumEngine.Domain.Services.Core.Dashboard;
using StadiumEngine.Jobs.Services.Builders.Dashboard;

namespace StadiumEngine.Jobs.Background.Dashboard;

internal class StadiumDashboardCalculator : DefaultJob, IStadiumDashboardCalculator
{
    private readonly IStadiumDashboardDataBuilder _builder;
    private readonly IStadiumDashboardCommandService _commandService;

    public StadiumDashboardCalculator(
        IStadiumDashboardDataBuilder builder,
        IStadiumDashboardCommandService commandService,
        ILogger<StadiumDashboardCalculator> logger ) : base( logger, "StadiumDashboardCalculator" )
    {
        _builder = builder;
        _commandService = commandService;
    }

    [Queue( "dashboards" )]
    public async Task CalculateAsync( int stadiumId )
    {
        LogInfo( $"Start calculate stadium {stadiumId}" );
        try
        {
            StadiumDashboard data = await _builder.BuildAsync( stadiumId, DateTime.Now );
            await _commandService.AddAsync( data );
        }
        catch ( Exception e )
        {
            LogError( $"Stadium {stadiumId} error", e );
        }

        LogInfo( $"Finish calculate stadium {stadiumId}" );
    }
}