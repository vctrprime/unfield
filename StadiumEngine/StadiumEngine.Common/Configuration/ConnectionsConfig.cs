using Microsoft.Extensions.Configuration;

namespace StadiumEngine.Common.Configuration;

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