using Microsoft.EntityFrameworkCore;
using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Repositories.Accounts;
using Unfield.Repositories.Infrastructure.Contexts;

namespace Unfield.Repositories.Accounts;

internal class UserStadiumRepository : BaseRepository<UserStadium>, IUserStadiumRepository
{
    public UserStadiumRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<UserStadium?> GetAsync( int userId, int stadiumId ) =>
        await Entities.FirstOrDefaultAsync( rs => rs.UserId == userId && rs.StadiumId == stadiumId );

    public new void Add( UserStadium userStadium ) => base.Add( userStadium );

    public new void Remove( UserStadium userStadium ) => base.Remove( userStadium );
}