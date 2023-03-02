using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Repositories.Rates;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Rates;

internal class PriceGroupRepository: BaseRepository<PriceGroup>, IPriceGroupRepository
{
    public PriceGroupRepository(MainDbContext context) : base(context)
    {
    }

    public async Task<List<PriceGroup>> GetAll(int stadiumId)
    {
        return await Entities
            .Include(pg => pg.Fields.Where(f => f.IsActive && !f.IsDeleted))
            .Where(pg => pg.StadiumId == stadiumId && !pg.IsDeleted).ToListAsync();
    }

    public async Task<PriceGroup?> Get(int priceGroupId,int stadiumId)
    {
        return await Entities
            .Include(pg => pg.Fields.Where(f => f.IsActive && !f.IsDeleted))
            .FirstOrDefaultAsync(pg => pg.Id == priceGroupId && pg.StadiumId == stadiumId && !pg.IsDeleted);
    }

    public new void Add(PriceGroup priceGroup)
    {
        base.Add(priceGroup);
    }

    public new void Update(PriceGroup priceGroup)
    {
        base.Update(priceGroup);
    }

    public new void Remove(PriceGroup priceGroup)
    {
        priceGroup.IsDeleted = true;
        base.Update(priceGroup);
    }
}