using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Unfield.Domain;
using Unfield.Domain.Entities;
using Unfield.Repositories.Infrastructure.Contexts;

namespace Unfield.Repositories.Infrastructure;

internal class ArchiveUnitOfWork : IArchiveUnitOfWork
{
    private readonly ArchiveDbContext _context;

    private IDbContextTransaction? _transaction;

    public ArchiveUnitOfWork( ArchiveDbContext context )
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
            if ( _transaction != null )
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync()
    {
        try
        {
            await _transaction?.RollbackAsync()!;
        }
        finally
        {
            if ( _transaction != null )
            {
                _transaction?.Dispose();
                _transaction = null;
            }
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