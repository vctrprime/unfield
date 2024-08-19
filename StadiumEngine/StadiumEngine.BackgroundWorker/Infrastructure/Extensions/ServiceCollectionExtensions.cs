using MassTransit;
using StadiumEngine.BackgroundWorker.Consumers.Bookings;
using StadiumEngine.Common.Configuration;
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
    /// <param name="connectionsConfig"></param>
    /// <param name="messagingConfig"></param>
    public static void RegisterModules( 
        this IServiceCollection services, 
        ConnectionsConfig connectionsConfig,
        MessagingConfig messagingConfig )
    {
        services.RegisterHandlers( connectionsConfig );
        services.AddEventBus( messagingConfig, ConfigureBusServices, ConfigureRabbit, true );
    }
    
    private static void ConfigureBusServices( IBusRegistrationConfigurator configurator )
    {
        configurator.AddConsumer<BookingConfirmedConsumer>();
    }

    private static void ConfigureRabbit( IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator configurator )
    {
        configurator.ReceiveEndpoint( "bookings",
            endpointConfigurator =>
            {
                endpointConfigurator.ConfigureConsumer<BookingConfirmedConsumer>( context );
            } );
    }
}