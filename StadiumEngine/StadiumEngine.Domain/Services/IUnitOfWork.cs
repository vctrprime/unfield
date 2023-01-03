using System.Threading.Tasks;

namespace StadiumEngine.Domain.Services;

public interface IUnitOfWork
{
    Task BeginTransaction();

    Task CommitTransaction();

    Task RollbackTransaction();

    Task SaveChanges();
}