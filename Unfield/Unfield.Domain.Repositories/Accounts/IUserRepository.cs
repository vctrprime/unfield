#nullable enable
using Unfield.Domain.Entities.Accounts;

namespace Unfield.Domain.Repositories.Accounts;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync( int stadiumGroupId );
    Task<User?> GetAsync( string login );
    Task<User?> GetAsync( int id );
    void Add( User user );
    void Update( User user );
    void Remove( User user );
}