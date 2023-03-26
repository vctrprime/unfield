using System.Threading.Tasks;
using StadiumEngine.Domain.Entities;

namespace StadiumEngine.Domain;

public interface IUnitOfWork
{
    Task BeginTransactionAsync();

    Task CommitTransactionAsync();
    
    Task SaveChangesAsync();

    bool PropertyWasChanged<T>( T obj, string propertyName ) where T : BaseEntity;
}