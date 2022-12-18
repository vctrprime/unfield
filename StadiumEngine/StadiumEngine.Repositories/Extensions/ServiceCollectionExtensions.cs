using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StadiumEngine.Data.Infrastructure.Contexts;
using StadiumEngine.Repositories.Abstract;
using StadiumEngine.Repositories.Concrete;

namespace StadiumEngine.Repositories.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterDataAccessModules(this IServiceCollection services)
    {
        services
            .RegisterContexts()
            .RegisterRepositories();
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

    private static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITestRepository, TestRepository>();
        
        return services;
    }
}