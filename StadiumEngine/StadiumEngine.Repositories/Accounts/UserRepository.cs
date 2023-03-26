using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Accounts;

internal class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<List<User>> GetAllAsync( int legalId ) =>
        await Entities
            .Where( u => u.LegalId == legalId && !u.IsDeleted && !u.IsSuperuser && !u.IsAdmin )
            .Include( u => u.Role )
            .Include( u => u.UserCreated )
            .Include( u => u.UserModified )
            .ToListAsync();

    public async Task<User?> GetAsync( string login ) => await GetAsync( u => u.PhoneNumber == login && !u.IsDeleted );

    public async Task<User?> GetAsync( int id ) => await GetAsync( u => u.Id == id && !u.IsDeleted );

    public new void Add( User user ) => base.Add( user );

    public new void Update( User user ) => base.Update( user );

    public new void Remove( User user )
    {
        user.IsDeleted = true;
        base.Update( user );
    }

    private async Task<User?> GetAsync( Expression<Func<User, bool>> predicate ) =>
        await Entities
            .Include( u => u.Role )
            .ThenInclude( r => r.RoleStadiums.Where( rs => !rs.Stadium.IsDeleted ) )
            .Include( u => u.Legal )
            .ThenInclude( l => l.Stadiums )
            .FirstOrDefaultAsync( predicate );
}