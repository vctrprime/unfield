using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Infrastructure;

internal class UnitOfWork : IUnitOfWork
{
    private readonly MainDbContext _context;

    private IDbContextTransaction? _transaction;

    public UnitOfWork( MainDbContext context )
    {
        _context = context;
    }

    public async Task BeginTransactionAsync() => _transaction = await _context.Database.BeginTransactionAsync();

    public async Task CommitTransactionAsync()
    {
        try
        {
            await SaveChangesAsync();
            await _transaction?.CommitAsync()!;
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            _transaction?.Dispose();
            _transaction = null;
        }
    }

    private async Task RollbackTransactionAsync()
    {
        try
        {
            await _transaction?.RollbackAsync()!;
        }
        finally
        {
            _transaction?.Dispose();
            _transaction = null;
        }
    }

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    public bool PropertyWasChanged<T>( T obj, string propertyName ) where T : BaseEntity
    {
        IEnumerable<EntityEntry> changedEntities =
            _context.ChangeTracker.Entries().Where( entity => entity.State == EntityState.Modified );
        
        foreach ( EntityEntry entry in changedEntities )
        {
            if ( entry.Entity.GetType() != obj.GetType() )
            {
                continue;
            }

            if ( ( int )( entry.Property( "Id" ).CurrentValue ?? 0 ) != obj.Id )
            {
                continue;
            }

            return entry.Property( propertyName ).CurrentValue != entry.Property( propertyName ).OriginalValue;
        }

        return false;
    }
}