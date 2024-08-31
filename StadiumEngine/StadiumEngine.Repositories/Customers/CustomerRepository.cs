using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.Customers;
using StadiumEngine.Domain.Repositories.Customers;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Customers;

internal class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<Customer?> GetAsync( string phoneNumber ) => 
        await Entities.SingleOrDefaultAsync( x => x.PhoneNumber == phoneNumber );

    public async Task<Customer?> GetAsync( int id ) => 
        await Entities.SingleOrDefaultAsync( x => x.Id == id );

    public new void Add( Customer customer ) => 
        base.Add( customer );
    

    public new void Update( Customer customer ) =>
        base.Update( customer );
}