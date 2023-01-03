using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Accounts;

internal class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(MainDbContext context) : base(context)
    {
    }

    public new void Add(Role role)
    {
        base.Add(role);
    }
}