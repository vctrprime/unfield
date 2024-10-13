using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Repositories.Accounts;
using Unfield.Repositories.Infrastructure.Contexts;

namespace Unfield.Repositories.Accounts;

internal class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<List<User>> GetAllAsync( int stadiumGroupId ) =>
        await Entities
            .Where( u => u.StadiumGroupId == stadiumGroupId && !u.IsDeleted && !u.IsSuperuser && !u.IsAdmin )
            .Include( r => r.UserStadiums.Where( rs => !rs.Stadium.IsDeleted ) )
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
            .Include( u => u.UserStadiums.Where( rs => !rs.Stadium.IsDeleted ) )
            .Include( u => u.Role )
            .Include( u => u.StadiumGroup )
            .ThenInclude( l => l.Stadiums )
            .FirstOrDefaultAsync( predicate );
}