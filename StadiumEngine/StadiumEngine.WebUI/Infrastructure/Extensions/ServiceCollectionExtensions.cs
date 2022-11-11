using Microsoft.Extensions.DependencyInjection;
using StadiumEngine.DataAccess.Connection.Abstract;
using StadiumEngine.DataAccess.Connection.Concrete;
using StadiumEngine.DataAccess.Repositories.Abstract;
using StadiumEngine.DataAccess.Repositories.Concrete;

namespace StadiumEngine.WebUI.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterModules(this IServiceCollection services)
        {
            services
                .RegisterServices()
                .RegisterRepositories();
        }

        private static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IConnectionCreator, PgConnectionCreator>();
            return services;
        }
        
        private static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITestRepository, TestRepository>();
            return services;
        }

    }
}