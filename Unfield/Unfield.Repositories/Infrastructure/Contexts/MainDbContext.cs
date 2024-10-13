using Microsoft.EntityFrameworkCore;
using Unfield.Domain.Entities.Notifications;
using Unfield.Domain.Entities.Rates;
using Unfield.Domain.Entities.Settings;
using Unfield.Repositories.Infrastructure.Configurations.Accounts;
using Unfield.Repositories.Infrastructure.Configurations.Bookings;
using Unfield.Repositories.Infrastructure.Configurations.Customers;
using Unfield.Repositories.Infrastructure.Configurations.Geo;
using Unfield.Repositories.Infrastructure.Configurations.Notifications;
using Unfield.Repositories.Infrastructure.Configurations.Offers;
using Unfield.Repositories.Infrastructure.Configurations.Rates;
using Unfield.Repositories.Infrastructure.Configurations.Settings;

namespace Unfield.Repositories.Infrastructure.Contexts;

internal class MainDbContext : DbContext
{
    public MainDbContext( DbContextOptions<MainDbContext> options )
        : base( options )
    {
    }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        #region accounts

        modelBuilder.ApplyConfiguration( new StadiumGroupConfiguration() );
        modelBuilder.ApplyConfiguration( new PermissionConfiguration() );
        modelBuilder.ApplyConfiguration( new PermissionGroupConfiguration() );
        modelBuilder.ApplyConfiguration( new RoleConfiguration() );
        modelBuilder.ApplyConfiguration( new RolePermissionConfiguration() );
        modelBuilder.ApplyConfiguration( new StadiumConfiguration() );
        modelBuilder.ApplyConfiguration( new UserConfiguration() );
        modelBuilder.ApplyConfiguration( new UserStadiumConfiguration() );

        #endregion

        #region bookings

        modelBuilder.ApplyConfiguration( new BookingConfiguration() );
        modelBuilder.ApplyConfiguration( new BookingCostConfiguration() );
        modelBuilder.ApplyConfiguration( new BookingCustomerConfiguration() );
        modelBuilder.ApplyConfiguration( new BookingInventoryConfiguration() );
        modelBuilder.ApplyConfiguration( new BookingLockerRoomConfiguration() );
        modelBuilder.ApplyConfiguration( new BookingPromoConfiguration() );
        modelBuilder.ApplyConfiguration( new BookingWeeklyExcludeDayConfiguration() );
        modelBuilder.ApplyConfiguration( new BookingTokenConfiguration() );

        #endregion

        #region geo

        modelBuilder.ApplyConfiguration( new CityConfiguration() );
        modelBuilder.ApplyConfiguration( new CountryConfiguration() );
        modelBuilder.ApplyConfiguration( new RegionConfiguration() );

        #endregion

        #region notifications

        modelBuilder.ApplyConfiguration( new UIMessageConfiguration() );
        modelBuilder.ApplyConfiguration( new UIMessageLastReadConfiguration() );
        modelBuilder.ApplyConfiguration( new UIMessageTextConfiguration() );

        #endregion

        #region offers

        modelBuilder.ApplyConfiguration( new FieldConfiguration() );
        modelBuilder.ApplyConfiguration( new InventoryConfiguration() );
        modelBuilder.ApplyConfiguration( new LockerRoomConfiguration() );
        modelBuilder.ApplyConfiguration( new OffersImageConfiguration() );
        modelBuilder.ApplyConfiguration( new OffersSportKindConfiguration() );

        #endregion

        #region rates

        modelBuilder.ApplyConfiguration( new DayIntervalConfiguration() );
        modelBuilder.ApplyConfiguration( new PriceConfiguration() );
        modelBuilder.ApplyConfiguration( new PriceGroupConfiguration() );
        modelBuilder.ApplyConfiguration( new PromoCodeConfiguration() );
        modelBuilder.ApplyConfiguration( new TariffConfiguration() );
        modelBuilder.ApplyConfiguration( new TariffDayIntervalConfiguration() );

        #endregion

        #region settings

        modelBuilder.ApplyConfiguration( new BreakConfiguration() );
        modelBuilder.ApplyConfiguration( new BreakFieldConfiguration() );
        modelBuilder.ApplyConfiguration( new MainSettingsConfiguration() );

        #endregion

        #region customers

        modelBuilder.ApplyConfiguration( new CustomerConfiguration() );

        #endregion

        modelBuilder.Entity<DayInterval>().Ignore( t => t.DateModified );
        modelBuilder.Entity<MainSettings>().Ignore( t => t.UserCreatedId );
        modelBuilder.Entity<MainSettings>().Ignore( t => t.UserCreated );
        modelBuilder.Entity<UIMessageText>().Ignore( t => t.DateCreated );
        modelBuilder.Entity<UIMessageText>().Ignore( t => t.DateModified );

        base.OnModelCreating( modelBuilder );
    }
}