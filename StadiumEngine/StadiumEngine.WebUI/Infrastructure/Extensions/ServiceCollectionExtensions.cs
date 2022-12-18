using Microsoft.Extensions.DependencyInjection;
using StadiumEngine.Handlers.Extensions;

namespace StadiumEngine.WebUI.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterModules(this IServiceCollection services)
        {
            services.RegisterHandlers();
        }
        
    }
}