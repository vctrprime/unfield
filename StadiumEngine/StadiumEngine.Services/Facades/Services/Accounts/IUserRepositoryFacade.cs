using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Services.Facades.Services.Accounts;

internal interface IUserRepositoryFacade
{
    Task<User?> GetUserAsync( string login );
    Task<User?> GetUserAsync( int userId );
    Task<List<User>> GetUsersAsync( int legalId );
    void AddUser( User user );
    void UpdateUser( User user );
    void RemoveUser( User user );

    Task<List<Legal>> GetLegalsAsync( string searchString );

    Task<Role?> GetRoleAsync( int roleId );

    Task<List<Permission>> GetPermissionsAsync();
    Task<List<Permission>> GetPermissionsAsync( int roleId );

    Task<List<Stadium>> GetStadiumsForLegalAsync( int legalId );
    Task<List<Stadium>> GetStadiumsForRoleAsync( int roleId );
}