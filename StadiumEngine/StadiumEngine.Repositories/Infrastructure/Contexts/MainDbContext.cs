using Microsoft.EntityFrameworkCore;
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
        //modelBuilder.Ignore<BaseEntity>();
        //modelBuilder.Ignore<BaseUserEntity>();

        /*foreach ( IMutableEntityType et in modelBuilder.Model.GetEntityTypes() )
        {
            if ( !et.ClrType.IsSubclassOf( typeof( BaseEntity ) ) )
            {
                continue;
            }

            //et.FindProperty( "DateCreated" )!.SetDefaultValueSql( "now()" );
            //et.FindProperty( "DateCreated" )!.ValueGenerated =
               // ValueGenerated.OnAdd;
        }*/

        //modelBuilder.ApplyConfiguration( new BaseEntityTypeConfiguration() );
        //modelBuilder.ApplyConfiguration( new BaseUserEntityTypeConfiguration() );
        
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
        
        #endregion

        #region geo
        
        //modelBuilder.ApplyConfiguration( new BaseGeoEntityConfiguration() );
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
        
        //modelBuilder.ApplyConfiguration( new BaseOfferEntityConfiguration() );
        modelBuilder.ApplyConfiguration( new FieldConfiguration() );
        modelBuilder.ApplyConfiguration( new InventoryConfiguration() );
        modelBuilder.ApplyConfiguration( new LockerRoomConfiguration() );
        modelBuilder.ApplyConfiguration( new OffersImageConfiguration() );
        modelBuilder.ApplyConfiguration( new OffersSportKindConfiguration() );

        #endregion
        
        #region rates
        
        //modelBuilder.ApplyConfiguration( new BaseRateEntityConfiguration() );
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

        base.OnModelCreating( modelBuilder );
    }

    #region geo

    //public DbSet<Country> Countries { get; set; }
    //public DbSet<Region> Regions { get; set; }
    //public DbSet<City> Cities { get; set; }

    #endregion

    #region accounts

    //public DbSet<Legal> Legals { get; set; }
    //public DbSet<PermissionGroup> PermissionGroups { get; set; }
    //public DbSet<Role> Roles { get; set; }
    //public DbSet<User> Users { get; set; }
    //public DbSet<Permission> Permissions { get; set; }
    //public DbSet<RolePermission> RolePermissions { get; set; }
    //public DbSet<UserStadium> UserStadiums { get; set; }
    //public DbSet<Stadium> Stadiums { get; set; }

    #endregion

    #region offers

    //public DbSet<LockerRoom> LockerRooms { get; set; }
    //public DbSet<Field> Fields { get; set; }
    //public DbSet<Inventory> Inventories { get; set; }
    //public DbSet<OffersImage> OffersImages { get; set; }
    //public DbSet<OffersSportKind> SportKinds { get; set; }

    #endregion

    #region rates

    //public DbSet<Tariff> Tariffs { get; set; }
    //public DbSet<DayInterval> DayIntervals { get; set; }
    //public DbSet<TariffDayInterval> TariffDayIntervals { get; set; }
    //public DbSet<Price> Prices { get; set; }
    //public DbSet<PriceGroup> PriceGroups { get; set; }
    //public DbSet<PriceGroup> PromoCodes { get; set; }

    #endregion

    #region settings

    //public DbSet<MainSettings> MainSettings { get; set; }
    //public DbSet<Break> Breaks { get; set; }
    //public DbSet<BreakField> BreakFields { get; set; }

    #endregion

    #region booking
    
    //public DbSet<Booking> Bookings { get; set; }
    //public DbSet<BookingCost> BookingCosts { get; set; }
    //public DbSet<BookingCustomer> BookingCustomers { get; set; }
    //public DbSet<BookingInventory> BookingInventories { get; set; }
    //public DbSet<BookingLockerRoom> BookingLockerRooms { get; set; }
    //public DbSet<BookingPromo> BookingPromos { get; set; }
    //public DbSet<BookingWeeklyExcludeDay> BookingWeeklyExcludeDays { get; set; }

    #endregion

    #region notifications
    
    //public DbSet<UIMessage> UIMessages { get; set; }
    //public DbSet<UIMessageText> UIMessageTexts { get; set; }
    //public DbSet<UIMessageLastRead> UIMessageLastReads { get; set; }
    
    #endregion
}