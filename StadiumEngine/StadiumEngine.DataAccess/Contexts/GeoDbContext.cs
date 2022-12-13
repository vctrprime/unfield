using Microsoft.EntityFrameworkCore;
using StadiumEngine.Entities.Domain.Geo;

namespace StadiumEngine.DataAccess.Contexts;

public class GeoDbContext : DbContext
{
    public DbSet<Country> Countries { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<City> Cities { get; set; }
        
    public GeoDbContext(DbContextOptions options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("geo");
    }
}