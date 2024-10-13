using Microsoft.Extensions.DependencyInjection;
using Unfield.Common.Configuration;
using Unfield.Common.Configuration.Infrastructure.Extensions;
using Unfield.Common.Configuration.Sections;
using Unfield.EventBus;
using Unfield.Handlers.Extensions;

namespace Unfield.Extranet.Infrastructure.Extensions;

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