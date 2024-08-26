using Microsoft.Extensions.Logging;
using StadiumEngine.Domain.Services.Core.Dashboard;

namespace StadiumEngine.Jobs.Recurring.Dashboard;

internal class ClearOutdatedStadiumDashboardJob : DefaultJob, IClearOutdatedStadiumDashboardJob
{
    private readonly IStadiumDashboardCommandService _commandService;

    public ClearOutdatedStadiumDashboardJob( 
        ILogger<ClearOutdatedStadiumDashboardJob> logger,
        IStadiumDashboardCommandService commandService ) : base( logger, "ClearOutdatedStadiumDashboard" )
    {
        _commandService = commandService;
    }

    public async Task Clear()
    {
        LogInfo( "Start job" );

        int clearedRows = await _commandService.DeleteByDateAsync( DateTime.Today.AddDays( -7 ) );
        
        LogInfo( $"Cleared {clearedRows} rows" );
        
        LogInfo( "Finish job" );
    }
}