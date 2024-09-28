using MassTransit;
using StadiumEngine.BackgroundWorker.Consumers.Bookings;
using StadiumEngine.Common.Configuration;
using StadiumEngine.Common.Configuration.Infrastructure.Extensions;
using StadiumEngine.Common.Configuration.Sections;
using StadiumEngine.EventBus;
using StadiumEngine.Handlers.Extensions;

namespace StadiumEngine.BackgroundWorker.Infrastructure.Extensions;

/// <summary>
///     Расширение для регистрации зависимостей
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Зарегистрировать все необходимые модули
    /// </summary>
    /// <param name="services"></param>
    /// <param name="loadConfigurationResult"></param>
    public static void RegisterModules( 
        this IServiceCollection services, 
        LoadConfigurationResult loadConfigurationResult )
    {
        services.RegisterHandlers( loadConfigurationResult.ConnectionsConfig );
        services.AddEventBus( loadConfigurationResult.MessagingConfig, ConfigureBusServices, ConfigureRabbit, true );
    }
    
    private static void ConfigureBusServices( IBusRegistrationConfigurator configurator )
    {
        configurator.AddConsumer<BookingConfirmedConsumer>();
        configurator.AddConsumer<BookingCanceledConsumer>();
    }

    private static void ConfigureRabbit( IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator configurator ) =>
        configurator.ReceiveEndpoint( "bookings",
            endpointConfigurator =>
            {
                endpointConfigurator.ConfigureConsumer<BookingConfirmedConsumer>( context );
                endpointConfigurator.ConfigureConsumer<BookingCanceledConsumer>( context );
            } );
}