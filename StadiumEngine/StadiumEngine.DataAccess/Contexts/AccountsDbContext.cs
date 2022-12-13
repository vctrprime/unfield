using Microsoft.EntityFrameworkCore;
using StadiumEngine.Entities.Domain.Accounts;

namespace StadiumEngine.DataAccess.Contexts;

public class AccountsDbContext : DbContext
{
    public DbSet<Legal> Legals { get; set; }
        
    public AccountsDbContext(DbContextOptions options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("accounts");
    }
}