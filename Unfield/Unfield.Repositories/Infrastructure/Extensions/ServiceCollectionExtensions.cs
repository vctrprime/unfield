using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Unfield.Common.Configuration;
using Unfield.Common.Configuration.Sections;
using Unfield.Domain;
using Unfield.Domain.Repositories.Accounts;
using Unfield.Domain.Repositories.Bookings;
using Unfield.Domain.Repositories.Customers;
using Unfield.Domain.Repositories.Dashboard;
using Unfield.Domain.Repositories.Geo;
using Unfield.Domain.Repositories.Notifications;
using Unfield.Domain.Repositories.Offers;
using Unfield.Domain.Repositories.Rates;
using Unfield.Domain.Repositories.Settings;
using Unfield.Repositories.Accounts;
using Unfield.Repositories.Bookings;
using Unfield.Repositories.Customers;
using Unfield.Repositories.Dashboard;
using Unfield.Repositories.Geo;
using Unfield.Repositories.Infrastructure.Contexts;
using Unfield.Repositories.Notifications;
using Unfield.Repositories.Offers;
using Unfield.Repositories.Rates;
using Unfield.Repositories.Settings;

namespace Unfield.Repositories.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterDataAccessModules( 
        this IServiceCollection services, 
        ConnectionsConfig connectionsConfig ) =>
        services
            .RegisterContexts( connectionsConfig )
            .RegisterRepositories();

    private static IServiceCollection RegisterContexts( 
        this IServiceCollection services, 
        ConnectionsConfig connectionsConfig )
    {
        services.AddDbContext<MainDbContext>(
            options =>
            {
                options.UseNpgsql(
                    connectionsConfig.MainDb,
                    o => o.UseQuerySplittingBehavior( QuerySplittingBehavior.SplitQuery ) );
            } );

        services.AddDbContext<ArchiveDbContext>(
            options =>
            {
                options.UseNpgsql(
                    connectionsConfig.ArchiveDb,
                    o => o.UseQuerySplittingBehavior( QuerySplittingBehavior.SplitQuery ) );
            } );
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IArchiveUnitOfWork, ArchiveUnitOfWork>();

        return services;
    }

    private static IServiceCollection RegisterRepositories( this IServiceCollection services )
    {
        #region accounts

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IStadiumRepository, StadiumRepository>();
        services.AddScoped<IStadiumGroupRepository, StadiumGroupRepository>();
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
        services.AddScoped<IBookingTokenRepository, BookingTokenRepository>();

        #endregion

        #region notifications

        services.AddScoped<IUIMessageRepository, UIMessageRepository>();
        services.AddScoped<IUIMessageLastReadRepository, UIMessageLastReadRepository>();

        #endregion

        #region dashboard

        services.AddScoped<IStadiumDashboardRepository, StadiumDashboardRepository>();
        
        #endregion

        #region customers

        services.AddScoped<ICustomerRepository, CustomerRepository>();

        #endregion

        return services;
    }
}