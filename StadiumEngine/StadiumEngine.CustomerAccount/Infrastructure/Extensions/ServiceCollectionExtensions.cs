using StadiumEngine.Common.Configuration.Infrastructure.Extensions;
using StadiumEngine.EventBus;
using StadiumEngine.Handlers.Extensions;

namespace StadiumEngine.CustomerAccount.Infrastructure.Extensions;

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
        services.AddEventBus( loadConfigurationResult.MessagingConfig );
    }
}