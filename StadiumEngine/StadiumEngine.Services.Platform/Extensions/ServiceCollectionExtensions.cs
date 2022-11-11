using Microsoft.Extensions.DependencyInjection;
using StadiumEngine.DataAccess.Connection.Abstract;
using StadiumEngine.DataAccess.Connection.Concrete;
using StadiumEngine.DataAccess.Repositories.Abstract;
using StadiumEngine.DataAccess.Repositories.Concrete;

namespace StadiumEngine.Services.Platform.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterServicesPlatform(this IServiceCollection services)
        {
            services
                .RegisterDbConnectionCreator()
                .RegisterRepositories();
        }
        private static IServiceCollection RegisterDbConnectionCreator(this IServiceCollection services)
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