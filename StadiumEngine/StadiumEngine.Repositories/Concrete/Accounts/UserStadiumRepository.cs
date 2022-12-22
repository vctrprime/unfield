using StadiumEngine.Common.Exceptions;
using StadiumEngine.Data.Infrastructure.Contexts;
using StadiumEngine.Entities.Domain.Offers;
using StadiumEngine.Repositories.Abstract.Accounts;

namespace StadiumEngine.Repositories.Concrete.Accounts;

internal class UserStadiumRepository : BaseRepository, IUserStadiumRepository
{
    public UserStadiumRepository(MainDbContext context) : base(context)
    {
    }

    public async Task<List<Stadium>> Get(int userId)
    {
        var user = await Context.Users.FindAsync(userId);

        if (user is null) throw new DomainException("User not found!");

        return user.IsSuperuser ? 
            user.Legal.Stadiums.ToList() : 
            user.Role.RoleStadiums.Select(rs => rs.Stadium).ToList();
    }
}