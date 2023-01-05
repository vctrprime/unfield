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
            .Include(r => r.Users.Where(u => !u.IsDeleted))
            .Include(r => r.RoleStadiums.Where(rs => !rs.Stadium.IsDeleted))
            .Include(r => r.UserCreated)
            .Include(r => r.UserModified)
            .ToListAsync();
    }

    public async Task<Role?> Get(int roleId)
    {
        return await Entities
            .Include(r => r.Users.Where(u => !u.IsDeleted))
            .Include(r => r.RoleStadiums.Where(rs => !rs.Stadium.IsDeleted))
            .FirstOrDefaultAsync(r => r.Id == roleId);
    }

    public new void Add(Role role)
    {
        base.Add(role);
    }
    
    public new void Update(Role role)
    {
        base.Update(role);
    }

    public new void Remove(Role role)
    {
        base.Remove(role);
    }
}