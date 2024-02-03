using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Repositories.Bookings;
using StadiumEngine.Domain.Repositories.Dashboard;
using StadiumEngine.Domain.Repositories.Geo;
using StadiumEngine.Domain.Repositories.Notifications;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Repositories.Rates;
using StadiumEngine.Domain.Repositories.Settings;
using StadiumEngine.Repositories.Accounts;
using StadiumEngine.Repositories.Bookings;
using StadiumEngine.Repositories.Dashboard;
using StadiumEngine.Repositories.Geo;
using StadiumEngine.Repositories.Infrastructure.Contexts;
using StadiumEngine.Repositories.Notifications;
using StadiumEngine.Repositories.Offers;
using StadiumEngine.Repositories.Rates;
using StadiumEngine.Repositories.Settings;

namespace StadiumEngine.Repositories.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterDataAccessModules( this IServiceCollection services, string connectionString ) =>
        services
            .RegisterContexts( connectionString )
            .RegisterRepositories();

    private static IServiceCollection RegisterContexts( this IServiceCollection services, string connectionString )
    {
        services.AddDbContext<MainDbContext>(
            options =>
            {
                options.UseNpgsql(
                    connectionString,
                    o => o.UseQuerySplittingBehavior( QuerySplittingBehavior.SplitQuery ) );
            } );
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
        services.AddScoped<IUserStadiumRepository, UserStadiumRepository>();
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
        services.AddScoped<IPromoCodeRepository, PromoCodeRepository>();

        #endregion

        #region settings

        services.AddScoped<IMainSettingsRepository, MainSettingsRepository>();
        services.AddScoped<IBreakRepository, BreakRepository>();
        services.AddScoped<IBreakFieldRepository, BreakFieldRepository>();

        #endregion

        #region geo

        services.AddScoped<ICityRepository, CityRepository>();

        #endregion

        #region booking

        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IBookingLockerRoomRepository, BookingLockerRoomRepository>();
        services.AddScoped<IBookingWeeklyExcludeDayRepository, BookingWeeklyExcludeDayRepository>();

        #endregion

        #region notifications

        services.AddScoped<IUIMessageRepository, UIMessageRepository>();
        services.AddScoped<IUIMessageLastReadRepository, UIMessageLastReadRepository>();

        #endregion

        #region dashboard

        services.AddScoped<IStadiumDashboardRepository, StadiumDashboardRepository>();
        
        #endregion

        return services;
    }
}