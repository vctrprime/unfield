using Microsoft.EntityFrameworkCore;
using Unfield.Domain.Entities.Customers;
using Unfield.Domain.Repositories.Customers;
using Unfield.Repositories.Infrastructure.Contexts;

namespace Unfield.Repositories.Customers;

internal class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<Customer?> GetAsync( string phoneNumber, int stadiumId ) => 
        await Entities
            .Include( x => x.StadiumGroup )
            .ThenInclude( x => x.Stadiums.Where( s => !s.IsDeleted ) )
            .SingleOrDefaultAsync( x => x.PhoneNumber == phoneNumber && x.StadiumGroup.Stadiums.Select( s => s.Id ).Contains( stadiumId ) );
    
    public async Task<Customer?> GetAsync( string phoneNumber, string stadiumToken ) => 
        await Entities
            .Include( x => x.StadiumGroup )
            .ThenInclude( x => x.Stadiums.Where( s => !s.IsDeleted ) )
            .SingleOrDefaultAsync( x => x.PhoneNumber == phoneNumber && x.StadiumGroup.Stadiums.Select( s => s.Token ).Contains( stadiumToken ) );

    public async Task<Customer?> GetAsync( int id ) => 
        await Entities
            .Include( x => x.StadiumGroup )
            .ThenInclude( x => x.Stadiums.Where( s => !s.IsDeleted ) )
            .SingleOrDefaultAsync( x => x.Id == id );

    public new void Add( Customer customer ) => 
        base.Add( customer );
    

    public new void Update( Customer customer ) =>
        base.Update( customer );
}