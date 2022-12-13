using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StadiumEngine.DataAccess.Contexts;
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
                .RegisterContexts()
                .RegisterRepositories();
        }

        private static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            return services;
        }
        
        private static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITestRepository, TestRepository>();
            return services;
        }

        private static IServiceCollection RegisterContexts(this IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            services.AddDbContext<MainDbContext>(options =>
            {
                options.UseLazyLoadingProxies().UseNpgsql(connectionString);
            });

            return services;
        }

    }
}