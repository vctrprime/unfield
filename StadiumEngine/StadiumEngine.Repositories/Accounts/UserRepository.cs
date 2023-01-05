using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Accounts;

internal class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(MainDbContext context) : base(context)
    {
    }

    public async Task<List<User>> GetAll(int legalId)
    {
        return await Entities
            .Where(u => u.LegalId == legalId && !u.IsDeleted && !u.IsSuperuser)
            .Include(u => u.Role)
            .Include(u => u.UserCreated)
            .Include(u => u.UserModified)
            .ToListAsync();
    }

    public async Task<User?> Get(string login)
    {
        return await Get(u => u.PhoneNumber == login && !u.IsDeleted);
    }

    public async Task<User?> Get(int id)
    {
        return await Get(u => u.Id == id && !u.IsDeleted);
    }

    public new void Add(User user)
    {
        base.Add(user);
    }

    public new void Update(User user)
    {
        base.Update(user);
    }

    private async Task<User?> Get(Expression<Func<User, bool>> predicate)
    {
        return await Entities
            .Include(u => u.Role)
            .ThenInclude(r => r.RoleStadiums.Where(rs => !rs.Stadium.IsDeleted))
            .Include(u => u.Legal)
            .ThenInclude(l => l.Stadiums)
            .FirstOrDefaultAsync(predicate);
    }
}