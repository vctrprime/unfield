using Microsoft.Extensions.DependencyInjection;
using StadiumEngine.Common.Configuration;
using StadiumEngine.EventBus;
using StadiumEngine.Handlers.Extensions;

namespace StadiumEngine.Extranet.Infrastructure.Extensions;

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
        services.AddEventBus( messagingConfig );
    }
}