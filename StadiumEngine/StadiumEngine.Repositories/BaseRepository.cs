using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities;

namespace StadiumEngine.Repositories;

internal abstract class BaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly DbSet<TEntity> Entities;
    private readonly DbContext _context;

    protected BaseRepository( DbContext context )
    {
        _context = context;
        Entities = context.Set<TEntity>();
    }

    protected void Add( TEntity entity ) => Entities.Add( entity );

    protected void Add( IEnumerable<TEntity> entities ) => Entities.AddRange( entities );

    protected void Remove( TEntity entity ) => Entities.Remove( entity );

    protected void Remove( IEnumerable<TEntity> entities ) => Entities.RemoveRange( entities );

    protected void Update( TEntity entity )
    {
        entity.DateModified = DateTime.Now.ToUniversalTime();

        Entities.Attach( entity );
        Entities.Entry( entity ).State = EntityState.Modified;
    }

    protected async Task Commit() => await _context.SaveChangesAsync();
}