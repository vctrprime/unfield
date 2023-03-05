using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Services.Facades.Services.Accounts;

internal interface IUserRepositoryFacade
{
    Task<User?> GetUser( string login );
    Task<User?> GetUser( int userId );
    Task<List<User>> GetUsers( int legalId );
    void AddUser( User user );
    void UpdateUser( User user );
    void RemoveUser( User user );

    Task<List<Legal>> GetLegals( string searchString );

    Task<Role?> GetRole( int roleId );

    Task<List<Permission>> GetPermissions();
    Task<List<Permission>> GetPermissions( int roleId );

    Task<List<Stadium>> GetStadiumsForLegal( int legalId );
    Task<List<Stadium>> GetStadiumsForRole( int roleId );
}