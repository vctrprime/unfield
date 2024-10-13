using System.Threading.Tasks;
using Unfield.Domain.Entities;

namespace Unfield.Domain;

public interface IUnitOfWork
{
    Task BeginTransactionAsync();

    Task CommitTransactionAsync();
    
    Task SaveChangesAsync();

    Task RollbackTransactionAsync();

    bool PropertyWasChanged<T>( T obj, string propertyName ) where T : BaseEntity;
}