using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StadiumEngine.Common.Configuration;

namespace StadiumEngine.EventBus;

public static class EventBusServiceCollectionExtensions
{
    private const uint StartTimeoutSeconds = 20;
    private const string ContentType = "application/vnd.masstransit+json";

    public static IServiceCollection RegisterDispatcher( this IServiceCollection services )
    {
        services.AddScoped<IEventBusMessageDispatcher, EventBusMessageDispatcher>();
        return services;
    }
    
    public static IServiceCollection AddEventBus(
        this IServiceCollection services,
        MessagingConfig configuration,
        Action<IBusRegistrationConfigurator> configureBus = null,
        Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator> configureRabbit = null,
        bool waitUntilStarted = false )
    {
        services.AddSingleton( configuration );
        services.AddOptions<MassTransitHostOptions>()
            .Configure(
                options =>
                {
                    options.WaitUntilStarted = waitUntilStarted;
                    options.StartTimeout = TimeSpan.FromSeconds( StartTimeoutSeconds );
                } );
        services.AddOptions<RabbitMqTransportOptions>()
            .Configure(
                options =>
                {
                    RabbitMqHostAddress hostAddress
                        = new RabbitMqHostAddress( new Uri( configuration.Server ) );

                    options.Host = hostAddress.Host;
                    options.Port = ( ushort )hostAddress.Port;
                    options.VHost = hostAddress.VirtualHost;

                    options.User = configuration.Username;
                    options.Pass = configuration.Password;
                } );

        services.AddMassTransit(
            busConfigurator =>
            {
                configureBus?.Invoke( busConfigurator );

                busConfigurator.UsingRabbitMq(
                    ( context, rabbitConfigurator ) =>
                    {
                        configureRabbit?.Invoke( context, rabbitConfigurator );

                        rabbitConfigurator.ConfigurePublish( ConfigurePublish );
                        rabbitConfigurator.UseNewtonsoftJsonSerializer();
                    } );
            } );

        return services;
    }

    private static void ConfigurePublish( IPublishPipeConfigurator publishConfigurator )
    {
        publishConfigurator.UseExecute(
            publishContext =>
            {
                publishContext.Headers.Set( "Content-Type", ContentType );
            } );
    }
}