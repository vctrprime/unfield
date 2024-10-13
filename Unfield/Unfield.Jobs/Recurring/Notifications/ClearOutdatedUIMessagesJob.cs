using Microsoft.Extensions.Logging;
using Unfield.Domain.Services.Core.Notifications;

namespace Unfield.Jobs.Recurring.Notifications;

internal class ClearOutdatedUIMessagesJob : DefaultJob, IClearOutdatedUIMessagesJob
{
    private readonly IUIMessageCommandService _commandService;
    private const int BatchLimit = 1000;

    public ClearOutdatedUIMessagesJob( 
        ILogger<ClearOutdatedUIMessagesJob> logger,
        IUIMessageCommandService commandService ) : base( logger, "ClearOutdatedUIMessages" )
    {
        _commandService = commandService;
    }

    public async Task Clear()
    {
        LogInfo( "Start job" );

        int result = 0;
        int batchNumber = 1;
        
        while ( true )
        {
            int clearedRows = await _commandService.DeleteByDateAsync( DateTime.Today.AddDays( -4 ), BatchLimit );
            
            LogInfo( $"Batch {batchNumber}. Cleared {clearedRows} rows" );
            
            result += clearedRows;

            if ( clearedRows == 0 )
            {
                break;
            }

            batchNumber++;
        }
        
        LogInfo( $"Total. Cleared {result} rows" );
        
        LogInfo( "Finish job" );
    }
}