using StadiumEngine.Common.Exceptions;
using StadiumEngine.Data.Infrastructure.Contexts;
using StadiumEngine.Entities.Domain;

namespace StadiumEngine.Repositories.Concrete;

internal abstract class BaseRepository
{
    protected readonly MainDbContext Context;

    protected BaseRepository(MainDbContext context)
    {
        Context = context;
    }

    protected async Task<T> Update<T>(T entity, T item) where T : BaseEntity
    {
        item.DateModified = DateTime.Now.ToUniversalTime();
        if (entity == null || item == null) throw new DomainException("Ошибка при обновлении!");
        
        Context.Entry(entity).CurrentValues.SetValues(item);
        await Context.SaveChangesAsync();
        
        return entity;
    }
}