using System.Threading.Tasks;
using StadiumEngine.Domain.Entities;

namespace StadiumEngine.Domain;

public interface IUnitOfWork
{
    Task BeginTransaction();

    Task CommitTransaction();
    
    Task SaveChanges();

    bool PropertyWasChanged<T>( T obj, string propertyName ) where T : BaseEntity;
}