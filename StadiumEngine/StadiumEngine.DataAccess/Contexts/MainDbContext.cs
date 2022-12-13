using Microsoft.EntityFrameworkCore;
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
        
    }
}