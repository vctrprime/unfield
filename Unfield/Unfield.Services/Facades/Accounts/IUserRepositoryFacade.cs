using Unfield.Domain.Entities.Accounts;

namespace Unfield.Services.Facades.Accounts;

internal interface IUserRepositoryFacade
{
    Task<User?> GetUserAsync( string login );
    Task<User?> GetUserAsync( int userId );
    Task<List<User>> GetUsersAsync( int stadiumGroupId );
    void AddUser( User user );
    void UpdateUser( User user );
    void RemoveUser( User user );

    Task<List<StadiumGroup>> GetStadiumGroupsAsync( string searchString );

    Task<Role?> GetRoleAsync( int roleId );

    Task<List<Permission>> GetPermissionsAsync();
    Task<List<Permission>> GetPermissionsAsync( int roleId );

    Task<List<Stadium>> GetStadiumsForStadiumGroupAsync( int stadiumGroupId );
    Task<List<Stadium>> GetStadiumsForUserAsync( int userId );
    Task<UserStadium?> GetUserStadiumAsync( int roleId, int stadiumId );
    void AddUserStadium( UserStadium userStadium );
    void RemoveUserStadium( UserStadium userStadium );
}