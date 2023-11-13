using Microsoft.Extensions.DependencyInjection;
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
    /// <param name="connectionString"></param>
    public static void RegisterModules( this IServiceCollection services, string connectionString ) => services.RegisterHandlers(connectionString);
}