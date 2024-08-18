using StadiumEngine.Handlers.Extensions;

namespace StadiumEngine.Portal.Infrastructure.Extensions;

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
    /// <param name="archiveConnectionString"></param>
    public static void RegisterModules( 
        this IServiceCollection services, 
        string connectionString,
        string archiveConnectionString ) 
        => services.RegisterHandlers( connectionString, archiveConnectionString );
}