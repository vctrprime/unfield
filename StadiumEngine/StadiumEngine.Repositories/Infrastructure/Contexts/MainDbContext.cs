using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Entities.Geo;

namespace StadiumEngine.Repositories.Infrastructure.Contexts;

internal class MainDbContext : DbContext
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
        }

        base.OnModelCreating(modelBuilder);
    }
}