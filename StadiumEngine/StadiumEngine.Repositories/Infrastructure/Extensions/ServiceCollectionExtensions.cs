using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Repositories.Accounts;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Infrastructure.Extensions;

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
            options.UseNpgsql(connectionString);
        });
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    private static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        #region accounts
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IStadiumRepository, StadiumRepository>();
        services.AddScoped<ILegalRepository, LegalRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IRoleStadiumRepository, RoleStadiumRepository>();
        services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
        services.AddScoped<IPermissionGroupRepository, PermissionGroupRepository>();
        #endregion
        
        return services;
    }
}