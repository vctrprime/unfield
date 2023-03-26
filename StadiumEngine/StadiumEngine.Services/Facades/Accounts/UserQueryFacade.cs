using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Services.Facades.Services.Accounts;

namespace StadiumEngine.Services.Facades.Accounts;

internal class UserQueryFacade : IUserQueryFacade
{
    private readonly IUserRepositoryFacade _userRepositoryFacade;

    public UserQueryFacade(
        IUserRepositoryFacade userRepositoryFacade
    )
    {
        _userRepositoryFacade = userRepositoryFacade;
    }


    public async Task<User?> GetUserAsync( int userId ) => await _userRepositoryFacade.GetUserAsync( userId );

    public async Task<List<User>> GetUsersByLegalIdAsync( int legalId ) => await _userRepositoryFacade.GetUsersAsync( legalId );

    public async Task<List<Permission>> GetUserPermissionsAsync( int userId )
    {
        User? user = await _userRepositoryFacade.GetUserAsync( userId );

        return user switch
        {
            { Role: { } } => await _userRepositoryFacade.GetPermissionsAsync( user.Role.Id ),
            _ => await _userRepositoryFacade.GetPermissionsAsync()
        };
    }

    public async Task<List<Stadium>> GetUserStadiumsAsync( int userId, int legalId )
    {
        User? user = await _userRepositoryFacade.GetUserAsync( userId );

        return user switch
        {
            { Role: { } } => await _userRepositoryFacade.GetStadiumsForRoleAsync( user.Role.Id ),
            _ => await _userRepositoryFacade.GetStadiumsForLegalAsync( legalId )
        };
    }
}