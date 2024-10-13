using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Filters;
using Unfield.Common.Configuration.Sections;

namespace Unfield.Common.Configuration.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static LoadConfigurationResult LoadConfigurations( 
        this IServiceCollection services,
        IConfiguration configuration )
    {
        services.AddSingleton( configuration.GetSection( "StorageConfig" ).Get<StorageConfig>() );
        services.AddSingleton( configuration.GetSection( "UtilsConfig" ).Get<UtilsConfig>() );
        services.AddSingleton( configuration.GetSection( "UtilServiceConfig" ).Get<UtilServiceConfig>() );
        services.AddSingleton( configuration.GetSection( "EnvConfig" ).Get<EnvConfig>() );
        
        MessagingConfig messagingConfig = configuration.GetSection( "MessagingConfig" ).Get<MessagingConfig>();
        ConnectionsConfig connectionsConfig = new ConnectionsConfig( configuration );

        return new LoadConfigurationResult
        {
            MessagingConfig = messagingConfig,
            ConnectionsConfig = connectionsConfig
        };
    }
}

public class LoadConfigurationResult
{
    public MessagingConfig MessagingConfig { get; set; }
    public ConnectionsConfig ConnectionsConfig { get; set; }
}