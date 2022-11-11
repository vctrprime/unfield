using Microsoft.Extensions.DependencyInjection;
using StadiumEngine.Services.Core.Actives.Abstract;
using StadiumEngine.Services.Core.Actives.Concrete;

namespace StadiumEngine.WebUI.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ICoreActivesService, CoreActivesService>();
        }

    }
}