using Microsoft.EntityFrameworkCore;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Accounts;

internal class UserStadiumRepository : BaseRepository, IUserStadiumRepository
{
    public UserStadiumRepository(MainDbContext context) : base(context)
    {
    }

    public async Task<List<Stadium>> Get(int userId)
    {
        var user = await Context.Users.FindAsync(userId);

        if (user is null) throw new DomainException("User not found!");

        return user.IsSuperuser
            ? await Context.Stadiums.Where(s => s.LegalId == user.LegalId).ToListAsync()
            : await Context.RoleStadiums.Where(rs => rs.RoleId == user.RoleId).Select(rs => rs.Stadium).ToListAsync();
    }
}