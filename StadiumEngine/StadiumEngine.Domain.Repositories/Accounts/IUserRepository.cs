#nullable enable
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Repositories.Accounts;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync( int legalId );
    Task<User?> GetAsync( string login );
    Task<User?> GetAsync( int id );
    void Add( User user );
    void Update( User user );
    void Remove( User user );
}