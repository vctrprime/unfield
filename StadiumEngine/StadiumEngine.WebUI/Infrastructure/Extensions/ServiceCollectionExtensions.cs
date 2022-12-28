using Microsoft.Extensions.DependencyInjection;
using StadiumEngine.Handlers.Extensions;

namespace StadiumEngine.WebUI.Infrastructure.Extensions
{
    /// <summary>
    /// Расширение для регистрации зависимостей
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Зарегистрировать все необходимые модули
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterModules(this IServiceCollection services)
        {
            services.RegisterHandlers();
        }
        
    }
}