using Microsoft.EntityFrameworkCore;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Accounts;

internal class UserStadiumRepository : BaseRepository<RoleStadium>, IUserStadiumRepository
{
    private DbSet<Stadium> _stadiums;
    
    public UserStadiumRepository(MainDbContext context) : base(context)
    {
        _stadiums = context.Set<Stadium>();
    }

    public async Task<List<Stadium>> Get(int roleId, int legalId, bool isSuperuser)
    {
        return isSuperuser
            ? await _stadiums.Where(s => s.LegalId == legalId).ToListAsync()
            : await Entities.Where(rs => rs.RoleId == roleId).Select(rs => rs.Stadium).ToListAsync();
    }
}