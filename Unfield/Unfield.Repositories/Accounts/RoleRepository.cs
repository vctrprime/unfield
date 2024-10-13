using Microsoft.EntityFrameworkCore;
using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Repositories.Accounts;
using Unfield.Repositories.Infrastructure.Contexts;

namespace Unfield.Repositories.Accounts;

internal class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<List<Role>> GetAllAsync( int stadiumGroupId ) =>
        await Entities
            .Where( r => r.StadiumGroupId == stadiumGroupId && !r.IsDeleted )
            .Include( r => r.Users.Where( u => !u.IsDeleted ) )
            .Include( r => r.UserCreated )
            .Include( r => r.UserModified )
            .ToListAsync();

    public async Task<Role?> GetAsync( int roleId ) =>
        await Entities
            .Include( r => r.Users.Where( u => !u.IsDeleted ) )
            .FirstOrDefaultAsync( r => r.Id == roleId && !r.IsDeleted );

    public new void Add( Role role ) => base.Add( role );

    public new void Update( Role role ) => base.Update( role );

    public new void Remove( Role role )
    {
        role.IsDeleted = true;
        base.Update( role );
    }
}