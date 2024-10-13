using Unfield.Common.Configuration;
using Unfield.Common.Configuration.Sections;

namespace Unfield.Portal.Infrastructure.Extensions;

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
    public static void RegisterModules( 
        this IServiceCollection services, 
        ConnectionsConfig connectionsConfig )
    {
        
    }
}