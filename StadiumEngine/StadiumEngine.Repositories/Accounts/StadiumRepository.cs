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
        return await Entities
            .Where(s => s.LegalId == legalId && !s.IsDeleted)
            .Include(s => s.City)
            .ThenInclude(c => c.Region)
            .ThenInclude(r => r.Country)
            .ToListAsync();
    }

    public async Task<List<Stadium>> GetForRole(int roleId)
    {
        return await Entities
            .Where(s => s.RoleStadiums.Select( rs => rs.RoleId).Contains(roleId) && !s.IsDeleted)
            .Include(s => s.City)
            .ThenInclude(c => c.Region)
            .ThenInclude(r => r.Country)
            .ToListAsync();
    }
    
}