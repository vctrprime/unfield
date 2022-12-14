using System.Linq;
using Microsoft.EntityFrameworkCore;
using StadiumEngine.Entities.Domain;
using StadiumEngine.Entities.Domain.Accounts;
using StadiumEngine.Entities.Domain.Geo;

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
    #endregion
    
        
    public MainDbContext(DbContextOptions<MainDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<BaseEntity>();
        
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