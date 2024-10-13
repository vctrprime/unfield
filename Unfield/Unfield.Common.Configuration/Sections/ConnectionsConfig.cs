using Microsoft.Extensions.Configuration;

namespace Unfield.Common.Configuration.Sections;

public class ConnectionsConfig
{
    public string MainDb { get; }
    public string ArchiveDb { get; }

    public ConnectionsConfig( IConfiguration configuration )
    {
        MainDb = configuration.GetConnectionString( "MainDbConnection" );
        ArchiveDb = configuration.GetConnectionString( "ArchiveDbConnection" );
    }
}