using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Accounts;

internal class RoleStadiumRepository : BaseRepository<RoleStadium>, IRoleStadiumRepository
{
    public RoleStadiumRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<RoleStadium?> Get( int roleId, int stadiumId )
    {
        return await Entities.FirstOrDefaultAsync( rs => rs.RoleId == roleId && rs.StadiumId == stadiumId );
    }

    public new void Add( RoleStadium roleStadium )
    {
        base.Add( roleStadium );
    }

    public new void Remove( RoleStadium roleStadium )
    {
        base.Remove( roleStadium );
    }
}