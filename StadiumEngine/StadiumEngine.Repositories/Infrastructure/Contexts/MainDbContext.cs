using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Entities.Geo;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Entities.Settings;

namespace StadiumEngine.Repositories.Infrastructure.Contexts;

internal class MainDbContext : DbContext
{
    public MainDbContext( DbContextOptions<MainDbContext> options )
        : base( options )
    {
    }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        modelBuilder.Ignore<BaseEntity>();
        modelBuilder.Ignore<BaseUserEntity>();

        foreach ( IMutableEntityType et in modelBuilder.Model.GetEntityTypes() )
        {
            if ( !et.ClrType.IsSubclassOf( typeof( BaseEntity ) ) )
            {
                continue;
            }

            et.FindProperty( "DateCreated" )!.SetDefaultValueSql( "now()" );
            et.FindProperty( "DateCreated" )!.ValueGenerated =
                ValueGenerated.OnAdd;
        }

        base.OnModelCreating( modelBuilder );
    }

    #region geo

    public DbSet<Country> Countries { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<City> Cities { get; set; }

    #endregion

    #region accounts

    public DbSet<Legal> Legals { get; set; }
    public DbSet<PermissionGroup> PermissionGroups { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<RoleStadium> RoleStadiums { get; set; }
    public DbSet<Stadium> Stadiums { get; set; }

    #endregion

    #region offers

    public DbSet<LockerRoom> LockerRooms { get; set; }
    public DbSet<Field> Fields { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<OffersImage> OffersImages { get; set; }
    public DbSet<OffersSportKind> SportKinds { get; set; }

    #endregion

    #region rates

    public DbSet<Tariff> Tariffs { get; set; }
    public DbSet<DayInterval> DayIntervals { get; set; }
    public DbSet<TariffDayInterval> TariffDayIntervals { get; set; }
    public DbSet<Price> Prices { get; set; }
    public DbSet<PriceGroup> PriceGroups { get; set; }
    public DbSet<PriceGroup> PromoCodes { get; set; }

    #endregion

    #region settings

    public DbSet<MainSettings> MainSettings { get; set; }
    public DbSet<Break> Breaks { get; set; }
    public DbSet<BreakField> BreakFields { get; set; }

    #endregion

    #region booking
    
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<BookingCost> BookingCosts { get; set; }
    public DbSet<BookingCustomer> BookingCustomers { get; set; }
    public DbSet<BookingInventory> BookingInventories { get; set; }
    public DbSet<BookingLockerRoom> BookingLockerRooms { get; set; }
    public DbSet<BookingPromo> BookingPromos { get; set; }

    #endregion
}