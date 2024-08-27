using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.Notifications;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Repositories.Infrastructure.Configurations.Accounts;
using StadiumEngine.Repositories.Infrastructure.Configurations.Bookings;
using StadiumEngine.Repositories.Infrastructure.Configurations.Geo;
using StadiumEngine.Repositories.Infrastructure.Configurations.Notifications;
using StadiumEngine.Repositories.Infrastructure.Configurations.Offers;
using StadiumEngine.Repositories.Infrastructure.Configurations.Rates;
using StadiumEngine.Repositories.Infrastructure.Configurations.Settings;

namespace StadiumEngine.Repositories.Infrastructure.Contexts;

internal class MainDbContext : DbContext
{
    public MainDbContext( DbContextOptions<MainDbContext> options )
        : base( options )
    {
    }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        #region accounts

        modelBuilder.ApplyConfiguration( new LegalConfiguration() );
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

        modelBuilder.Entity<DayInterval>().Ignore( t => t.DateModified );
        modelBuilder.Entity<MainSettings>().Ignore( t => t.UserCreatedId );
        modelBuilder.Entity<MainSettings>().Ignore( t => t.UserCreated );
        modelBuilder.Entity<UIMessageText>().Ignore( t => t.DateCreated );
        modelBuilder.Entity<UIMessageText>().Ignore( t => t.DateModified );

        base.OnModelCreating( modelBuilder );
    }
}