using System.Linq;
using Microsoft.EntityFrameworkCore;
using StadiumEngine.Entities.Domain;
using StadiumEngine.Entities.Domain.Accounts;
using StadiumEngine.Entities.Domain.Geo;
using StadiumEngine.Entities.Domain.Offers;

namespace StadiumEngine.DataAccess.Contexts;

public class MainDbContext : DbContext
{
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
    #endregion

    #region offers
    public DbSet<Stadium> Stadiums { get; set; }
    #endregion
        
    public MainDbContext(DbContextOptions<MainDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<BaseEntity>();
        modelBuilder.Ignore<BaseUserEntity>();
        
        foreach (var et in modelBuilder.Model.GetEntityTypes())
        {
            if (!et.ClrType.IsSubclassOf(typeof(BaseEntity))) continue;
            
            et.FindProperty("DateCreated")!.SetDefaultValueSql("now()");
            et.FindProperty("DateCreated")!.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAdd;

            et.FindProperty("DateModified")!.SetDefaultValueSql("now()");
            et.FindProperty("DateModified")!.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAddOrUpdate;
        }

        base.OnModelCreating(modelBuilder);
    }
}