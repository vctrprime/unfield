using Hangfire;
using Microsoft.Extensions.Logging;
using Unfield.Domain.Entities.Dashboard;
using Unfield.Domain.Services.Core.Dashboard;
using Unfield.Jobs.Services.Builders.Dashboard;

namespace Unfield.Jobs.Background.Dashboard;

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