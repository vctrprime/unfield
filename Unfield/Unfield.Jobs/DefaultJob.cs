using Microsoft.Extensions.Logging;

namespace Unfield.Jobs;

internal abstract class DefaultJob
{
    protected readonly ILogger _logger;
    private readonly string _prefix;

    protected DefaultJob( ILogger logger, string prefix )
    {
        _logger = logger;
        _prefix = prefix;
    }

    protected void LogInfo( string text ) => _logger.LogInformation( $"[{_prefix}] {text}" );

    protected void LogError( string text, Exception e ) => _logger.LogError( e, text );
}