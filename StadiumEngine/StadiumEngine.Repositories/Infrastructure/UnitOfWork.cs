using Microsoft.EntityFrameworkCore.Storage;
using StadiumEngine.Domain.Services;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Infrastructure;

internal class UnitOfWork : IUnitOfWork
{
    private readonly MainDbContext _context;
    
    private IDbContextTransaction? _transaction;

    public UnitOfWork(MainDbContext context)
    {
        _context = context;
    }

    public async Task BeginTransaction()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransaction()
    {
        try
        {
            await Commit();
            await _transaction?.CommitAsync()!;
        }
        catch
        {
            await RollbackTransaction();
            throw;
        }
        finally
        {
            _transaction?.Dispose();
            _transaction = null;
        }
    }

    public async Task RollbackTransaction()
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

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }
}