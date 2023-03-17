using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Repositories.Rates;
using StadiumEngine.Domain.Repositories.Settings;
using StadiumEngine.Repositories.Accounts;
using StadiumEngine.Repositories.Infrastructure.Contexts;
using StadiumEngine.Repositories.Offers;
using StadiumEngine.Repositories.Rates;
using StadiumEngine.Repositories.Settings;

namespace StadiumEngine.Repositories.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterDataAccessModules( this IServiceCollection services ) =>
        services
            .RegisterContexts()
            .RegisterRepositories();

    private static IServiceCollection RegisterContexts( this IServiceCollection services )
    {
        string? connectionString = Environment.GetEnvironmentVariable( "DB_CONNECTION_STRING" );
        services.AddDbContext<MainDbContext>( options => { options.UseNpgsql( connectionString ); } );
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    private static IServiceCollection RegisterRepositories( this IServiceCollection services )
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

        #region offers

        services.AddScoped<ILockerRoomRepository, LockerRoomRepository>();
        services.AddScoped<IFieldRepository, FieldRepository>();
        services.AddScoped<IInventoryRepository, InventoryRepository>();
        services.AddScoped<IOffersImageRepository, OffersImageRepository>();
        services.AddScoped<IOffersSportKindRepository, OffersSportKindRepository>();

        #endregion

        #region rates

        services.AddScoped<IPriceGroupRepository, PriceGroupRepository>();
        services.AddScoped<ITariffDayIntervalRepository, TariffDayIntervalRepository>();
        services.AddScoped<IDayIntervalRepository, DayIntervalRepository>();
        services.AddScoped<ITariffRepository, TariffRepository>();
        services.AddScoped<IPriceRepository, PriceRepository>();

        #endregion

        #region settings

        services.AddScoped<IStadiumMainSettingsRepository, StadiumMainSettingsRepository>();

        #endregion

        return services;
    }
}