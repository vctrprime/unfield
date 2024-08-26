using Microsoft.Extensions.Logging;
using StadiumEngine.Domain.Services.Core.Schedule;

namespace StadiumEngine.Jobs.Recurring.Bookings;

internal class ClearOutdatedBookingDraftsJob : DefaultJob, IClearOutdatedBookingDraftsJob
{
    private readonly ISchedulerBookingCommandService _bookingCommandService;
    private const int BatchLimit = 1000;

    public ClearOutdatedBookingDraftsJob( 
        ILogger<ClearOutdatedBookingDraftsJob> logger,
        ISchedulerBookingCommandService bookingCommandService ) : base( logger, "ClearOutdatedBookingDraftsJob" )
    {
        _bookingCommandService = bookingCommandService;
    }

    public async Task Clear()
    {
        LogInfo( "Start job" );

        int result = 0;
        int batchNumber = 1;
        
        while ( true )
        {
            int clearedRows = await _bookingCommandService.DeleteDraftsByDateAsync( DateTime.Today.AddDays( -2 ), BatchLimit );
            
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