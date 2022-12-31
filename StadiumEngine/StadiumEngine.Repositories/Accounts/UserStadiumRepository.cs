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

    public async Task<List<Stadium>> Get(int userId)
    {
        var user = await Users.FindAsync(userId);

        if (user is null) throw new DomainException("User not found!");

        return user.IsSuperuser
            ? await _stadiums.Where(s => s.LegalId == user.LegalId).ToListAsync()
            : await Entities.Where(rs => rs.RoleId == user.RoleId).Select(rs => rs.Stadium).ToListAsync();
    }
}