using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Accounts;

internal class RoleStadiumRepository : BaseRepository<RoleStadium>, IRoleStadiumRepository
{
    public RoleStadiumRepository(MainDbContext context) : base(context)
    {
    }

    public new void Add(RoleStadium roleStadium)
    {
        base.Add(roleStadium);
    }
    
}