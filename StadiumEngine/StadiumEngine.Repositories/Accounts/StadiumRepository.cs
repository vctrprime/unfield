using Microsoft.EntityFrameworkCore;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Accounts;

internal class StadiumRepository : BaseRepository<Stadium>, IStadiumRepository
{
    public StadiumRepository(MainDbContext context) : base(context)
    {
        
    }
    
    public async Task<List<Stadium>> GetForLegal(int legalId)
    {
        return await Entities.Where(s => s.LegalId == legalId).ToListAsync();
    }

    public async Task<List<Stadium>> GetForRole(int roleId)
    {
        return await Entities
            .Include(s => s.RoleStadiums)
            .Where(s => s.RoleStadiums.Select( rs => rs.RoleId).Contains(roleId))
            .ToListAsync();
    }

    public new void Add(IEnumerable<Stadium> stadiums)
    {
        base.Add(stadiums);
    }
}