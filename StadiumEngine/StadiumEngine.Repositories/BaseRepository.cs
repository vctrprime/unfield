using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Repositories;

internal abstract class BaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly DbSet<TEntity> Entities;
    protected readonly DbSet<User> Users;

    protected BaseRepository(DbContext context)
    {
        Entities = context.Set<TEntity>();
        Users = context.Set<User>();
    }

    protected void Add(TEntity entity)
    {
        Entities.AddAsync(entity);
    }
    
    protected void Add(IEnumerable<TEntity> entities)
    {
        Entities.AddRangeAsync(entities);
    }
    
    protected void Remove(TEntity entity)
    {
        Entities.Remove(entity);
    }
    
    protected void Remove(IEnumerable<TEntity> entities)
    {
        Entities.RemoveRange(entities);
    }

    protected void Update(TEntity entity)
    {
        entity.DateModified = DateTime.Now.ToUniversalTime();
        
        Entities.Attach(entity);
        Entities.Entry(entity).State = EntityState.Modified;
    }
    
}