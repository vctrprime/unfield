using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Accounts;

internal class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(MainDbContext context) : base(context)
    {
    }
    
    public async Task<List<Role>> GetAll(int legalId)
    {
        return await Entities
            .Where(u => u.LegalId == legalId)
            .Include(u => u.UserCreated)
            .Include(u => u.UserModified)
            .ToListAsync();
    }
    
    public new void Add(Role role)
    {
        base.Add(role);
    }
}