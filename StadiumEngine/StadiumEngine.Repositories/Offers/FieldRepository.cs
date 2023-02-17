using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Offers;

internal class FieldRepository : BaseRepository<Field>, IFieldRepository
{
    public FieldRepository(MainDbContext context) : base(context)
    {
    }

    public async Task<List<Field>> GetAll(int stadiumId)
    {
        return await Entities
            .Where(f => f.StadiumId == stadiumId && !f.IsDeleted)
            .Include(f => f.FieldSportKinds)
            .Include(f => f.Images)
            .ToListAsync();
    }

    public async Task<Field?> Get(int fieldId,int stadiumId)
    {
        return await Entities
            .Include(f => f.FieldSportKinds)
            .Include(f => f.Images).FirstOrDefaultAsync(f => f.Id == fieldId && f.StadiumId == stadiumId && !f.IsDeleted);
    }

    public new void Add(Field field)
    {
        base.Add(field);
    }

    public new void Update(Field field)
    {
        base.Update(field);
    }

    public new void Remove(Field field)
    {
        field.IsDeleted = true;
        base.Update(field);
    }
}