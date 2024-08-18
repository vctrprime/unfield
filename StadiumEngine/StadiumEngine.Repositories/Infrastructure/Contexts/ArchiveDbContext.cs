using Microsoft.EntityFrameworkCore;
using StadiumEngine.Repositories.Infrastructure.Configurations.Dashboard;

namespace StadiumEngine.Repositories.Infrastructure.Contexts;

internal class ArchiveDbContext : DbContext
{
    public ArchiveDbContext( DbContextOptions<ArchiveDbContext> options )
        : base( options )
    {
    }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        modelBuilder.ApplyConfiguration( new StadiumDashboardConfiguration() );
    }
}